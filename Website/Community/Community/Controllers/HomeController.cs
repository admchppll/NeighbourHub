using Community.Models;
using Ganss.XSS;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
                contactNew.Replied = false;

                db.Contacts.Add(contactNew);
                db.SaveChanges();

                ViewBag.Success = true;
                return View();
            }
            return View(contact);
        }
    }
}