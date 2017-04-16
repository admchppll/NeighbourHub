using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community.Controllers
{
    public class AdminController : Controller
    {
        CommunityEntities db = new CommunityEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactTile() {
            ContactPartialView model = new ContactPartialView();
            model.TotalOpen = db.Contacts.Where(c => c.LinkedEmail == null && c.Replied == false).Count();
            model.Contacts = db.Contacts.Where(c => c.LinkedEmail == null && c.Replied == false).OrderBy(c => c.Date).Take(3).ToList();
            return View(model);
        }

        public ActionResult ReportPartial() {
            ReportPartialView model = new ReportPartialView();



            return PartialView();
        }

    }
}
