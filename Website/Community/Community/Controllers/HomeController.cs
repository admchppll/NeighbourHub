using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community.Controllers
{
    public class HomeController : Controller
    {
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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include="Name, Email, ConfirmEmail, Message")] ContactModel contact)
        {
            ViewBag.Message = "Your contact page.";

            if (ModelState.IsValid) {

            }

            return View(contact);
        }
    }
}