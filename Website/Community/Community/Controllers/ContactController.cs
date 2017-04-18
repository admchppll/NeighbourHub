using Community.Helpers;
using Community.Models;
using Ganss.XSS;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Community.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        CommunityEntities db = new CommunityEntities();
        public const int pageSize = 20;

        // GET: Contact
        public ActionResult Index(int? page, string search, bool? all, string sortOrder)
        {
            int pageNumber = page ?? 1;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.Search = search;
            ViewBag.ShowAll = all == null ? false : all; 

            var contacts = db.Contacts.Select(c => c);

            if (all != true)
            {
                contacts = contacts.Where(c => c.Replied == false && c.LinkedEmail == null);
            }
            else {
                contacts = contacts.Where(c => c.LinkedEmail == null);
            }

            if (search != null && search != "") {
                contacts = contacts.Where(c => c.Email.ToLower().Contains(search) || c.Name.ToLower().Contains(search));
            }

            switch (sortOrder) {
                case "Name":
                    contacts = contacts.OrderBy(c => c.Name);
                    break;
                case "name_desc":
                    contacts = contacts.OrderByDescending(c => c.Name);
                    break;
                case "Email":
                    contacts = contacts.OrderBy(c => c.Email);
                    break;
                case "email_desc":
                    contacts = contacts.OrderByDescending(c => c.Email);
                    break;
                case "date_desc":
                    contacts = contacts.OrderByDescending(c => c.Date);
                    break;
                default:
                    contacts = contacts.OrderBy(c => c.Date);
                    break;
            }

            return View(contacts.ToPagedList(pageNumber,pageSize));
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contact/Reply
        public ActionResult Reply(int id)
        {
            ContactDisplayModel model = new ContactDisplayModel
            {
                Contact = db.Contacts.Find(id),
                Replies = db.Contacts.Where(c => c.LinkedEmail == id).ToList()
            };
            return View(model);
        }

        // POST: Contact/Reply
        [HttpPost]
        public ActionResult Reply([Bind(Include ="ContactID, Message")]ContactReply reply)
        {
            // TODO: Add insert logic here
            var contact = db.Contacts.Find(reply.ContactID);
            var userId = User.Identity.GetUserId();
            var profile = db.Profiles.Where(u => u.UserID == userId).Single();

            var sanitizer = new HtmlSanitizer();
            reply.Message = sanitizer.Sanitize(reply.Message);

            string message = String.Format("{0}<p>Your initial message can be seen below:</p><br/>{1}<br/>", reply.Message, contact.Message);
            
            EmailExtendedModel email = new EmailExtendedModel
            {
                RecipientName = contact.Name,
                SenderName = String.Format("{0} {1}", profile.FirstName, profile.Surname),
                From = ConfigurationManager.AppSettings["DefaultEmail"],
                To = contact.Email,
                Subject = String.Format("Re: Enquiry #{0}", contact.ID),
                Message = message,
                ReplyEmail = ConfigurationManager.AppSettings["ReplyEmail"]
            };
            email.SendAsync();

            contact.Replied = true;
            Contact newContact = new Contact {
                Name = contact.Name,
                Email = contact.Email,
                Message = reply.Message,
                LinkedEmail = contact.ID,
                Replied = false,
                Date = DateTime.Now
            };

            db.Contacts.Add(newContact);
            db.SaveChanges();

            AuditHelper.AddAudit(userId, String.Format("[Admin] Replied to contact form submission #{0}. Reply ID: #{0}", contact.ID, newContact.ID));
            return RedirectToAction("Index");

        }

        
    }
}
