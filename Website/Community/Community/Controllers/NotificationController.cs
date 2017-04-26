using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;

namespace Community.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Notification
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var notifications = db.Notifications
                .Include(n => n.User)
                .Where(n => n.UserID == userId)
                .OrderBy(n => n.ID);

            return View(notifications.ToList());
        }

        public ActionResult UserPartial()
        {
            string userId = User.Identity.GetUserId();

            var notifications = db.Notifications
                .Include(n => n.User)
                .Where(n => n.UserID == userId)
                .OrderBy(n => n.ID);
            return View(notifications.ToList());
        }

        public class NotificationData {
            public int ID { get; set; }
        }

        public JsonResult Read(NotificationData data) {
            Notification notification = db.Notifications.Find(data.ID);
            notification.Viewed = true;
            //db.Notifications.Remove(notification);
            db.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
