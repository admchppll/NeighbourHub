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
    public class AuditController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Audit
        public ActionResult Index()
        {
            var audits = db.Audits
                .Include(a => a.Event)
                .Include(a => a.User);
            return View(audits.ToList());
        }

        // GET: Audit/AuditUser
        public ActionResult AuditUser(string userId)
        {
            var audits = db.Audits
                .Include(a => a.User)
                .Where(a => a.UserID == userId);
            return PartialView(audits.ToList());
        }

        // GET: Audit/AuditEvent
        public ActionResult AuditEvent(int eventId)
        {
            var audits = db.Audits
                .Include(a => a.Event)
                .Include(a => a.User)
                .Where(a => a.EventID == eventId);
            return PartialView(audits.ToList());
        }

        // GET: Audit/AuditReport
        public ActionResult AuditReport(int reportId)
        {
            var audits = db.Audits
                .Include(a => a.User)
                .Where(a => a.ReportID == reportId);

            ViewBag.ReportID = reportId;

            return PartialView(audits.ToList());
        }

        // GET: Audit/ReportCreate
        public ActionResult ReportCreate(int ReportID)
        {
            var report = db.Reports.Find(ReportID);
            ViewBag.EventID = report.ReportedEvent;
            ViewBag.ReportID = ReportID;

            return PartialView();
        }

        // POST: Audit/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateAudit([Bind(Include = "ID,Date,UserID,EventID,AuditMessage,ReportID")] Audit audit)
        {
            audit.UserID = User.Identity.GetUserId();
            audit.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Audits.Add(audit);
                db.SaveChanges();

                //if (audit.EventID == null)
                //{
                    return RedirectToAction("Details", "Report", new { id = audit.ReportID });
                //}
                //else {
                    //return RedirectToAction("Event", "Report", new { ReportID = audit.ReportID });
                //}
            }

            return View(audit);
        }

        // GET: Audit/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID");
            return View();
        }

        // POST: Audit/Create
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,UserID,EventID,AuditMessage,ReportID")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                db.Audits.Add(audit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", audit.EventID);
            return View(audit);
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
