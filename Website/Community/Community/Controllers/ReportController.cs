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
using PagedList;
using Community.Helpers;

namespace Community.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private CommunityEntities db = new CommunityEntities();
        private const int pageSize = 15;

        // GET: Report
        public ActionResult Index(int? page, string section, bool? showResolved)
        {
            int pageNumber = (page ?? 1);
            showResolved = showResolved != null ? showResolved : false;
            ViewBag.Section = string.IsNullOrEmpty(section) ? "index" : section;
            ViewBag.PageNumber = pageNumber;
            ViewBag.ShowResolved = showResolved;

            var reports = db.Reports
                .Include(r => r.Event)
                .Include(r => r.User)
                .Include(r => r.User1);

            switch (section)
            {
                case "event":
                    reports = reports.Where(r => r.ReportedEvent != null && r.ReportedID == null);
                    break;
                case "user":
                    reports = reports.Where(r => r.ReportedEvent == null && r.ReportedID != null);
                    break;
                default:
                    break;
            }

            if (showResolved == false) {
                reports = reports.Where(r => r.ResolvedDate != null);
            }

            reports = reports.OrderBy(m => m.ID);

            return View(reports.ToPagedList(pageNumber, pageSize));
        }

        // GET: Report/Confirmation
        public ActionResult Confirmation()
        {
            return View();
        }

        // GET: Report/Reply
        public ActionResult Reply(int reportID)
        {
            Report report = db.Reports.Find(reportID);
            ViewBag.RecipientID = report.UserID;
            ViewBag.Title = "RE: Report #" + report.ID;
            ViewBag.ReportID = reportID;
            return PartialView("Reply");
        }

        //POST: Report/MessageCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MessageCreate([Bind(Include = "ID,RecipientID,Title,Body,Admin")] Message message, int ReportID)
        {
            message.SenderID = User.Identity.GetUserId();
            message.Read = false;

            if (message.Saved == false)
            {
                message.Sent = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();

                
                    message.Sent = DateTime.Now;
                    NotificationHelper.Create(
                        message.RecipientID,
                        "New Message",
                        "You have received a new message!",
                        "~/Message/Read/" + message.ID);

                AuditHelper.AddUserAudit(message.SenderID, "Message sent to reporter:" + message.Body, ReportID);


                return RedirectToAction("Details", "Report", new { id = ReportID });
            }

            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            return View(message);
        }

        // GET: Report/Event
        public ActionResult Event(int? eventId)
        {
            var @event = db.Events.Find(eventId);
            ViewBag.EventTitle = @event.Title;
            ViewBag.ReportedEvent = eventId;

            return View();
        }

        // POST: Report/Event
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Event([Bind(Include = "ID,ReportedEvent,Description")] Report report)
        {
            report.UserID = User.Identity.GetUserId();
            report.Sent = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();

                int repId = report.ReportedEvent ?? 0;

                AuditHelper.AddEventAudit(repId, "Event has been reported. Report ID: " + report.ID, report.ID);
                return RedirectToAction("Confirmation");
            }

            ViewBag.ReportedID = report.ReportedID;
            return View(report);
        }

        // GET: Report
        public ActionResult ReportUser(string userId)
        {
            var user = db.Users.Where(u => u.ID == userId).Single();
            ViewBag.ReportedID = userId;
            ViewBag.UserName = user.UserName;

            return View();
        }

        // POST: Report/ReportUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportUser([Bind(Include = "ID,ReportedID,Description")] Report report)
        {
            report.UserID = User.Identity.GetUserId();
            report.Sent = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();

                AuditHelper.AddUserAudit(report.ReportedID, "User has been reported. Report ID: " + report.ID, report.ID);
                return RedirectToAction("Confirmation");
            }

            ViewBag.ReportedID = report.ReportedID;
            return View(report);
        }

        // GET: Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            ViewBag.ReportedEvent = new SelectList(db.Events, "ID", "HostID");
            ViewBag.ReportedID = new SelectList(db.Users, "ID", "Email");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,ReportedEvent,ReportedID,Description")] Report report)
        {
            report.Sent = DateTime.Now;
            report.ResolvedDate = null;

            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ReportedEvent = new SelectList(db.Events, "ID", "HostID", report.ReportedEvent);
            ViewBag.ReportedID = new SelectList(db.Users, "ID", "Email", report.ReportedID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", report.UserID);
            return View(report);
        }

        public class ReportData
        {
            public int ID { get; set; }
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Resolve(ReportData data)
        {
            Report report = db.Reports.Find(data.ID);

            if (report.ResolvedDate != null) {
                return Json(new
                {
                    success = false,
                    title = "Resolved! ",
                    message = "This report has already been marked as resolved!"
                });
            }

            report.ResolvedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    title = "Success! ",
                    message = "This report has been marked resolved!"
                });
            }

            return Json(new
            {
                success = false,
                title = "Something went wrong! ",
                message = "We couldn't resolve this report."
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
