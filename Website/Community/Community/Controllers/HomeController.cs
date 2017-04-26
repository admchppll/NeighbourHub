using Community.Helpers;
using Community.Models;
using Ganss.XSS;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community.Controllers
{
    public class HomeController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        public ActionResult Index()
        {
            ViewBag.FullWidth = true;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.FullWidth = true;
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, CaptchaValidator]
        public ActionResult Contact([Bind(Include="Name, Email, ConfirmEmail, Message")] ContactModel contact, bool captchaValid)
        {
            var sanitizer = new HtmlSanitizer();

            if (ModelState.IsValid) {
                Contact contactNew = new Contact();
                contactNew.Name = contact.Name;
                contactNew.Email = contact.Email;
                contactNew.Message = sanitizer.Sanitize(contact.Message);
                contactNew.Date = DateTime.Now;
                contactNew.Replied = false;

                db.Contacts.Add(contactNew);
                db.SaveChanges();

                string Message = String.Format("<p>Thank you for your message today. At NeighbourHub we always endevour to deliver the best possible service, therefore we always try to respond within a maximum of 3 working days. If you have not received a response by this time please email info@theneighbourhub.online directly referencing #{0} in the subject line. You can find details of your enquiry below:</p><div>{1}</div>", 
                    contactNew.ID, 
                    contactNew.Message);

                EmailHelper.Create(contactNew.Email, "Enquiry #" + contactNew.ID, Message);

                ViewBag.Success = true;
                return View();
            }
            return View(contact);
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Cookies()
        {
            return View();
        }
    }
}