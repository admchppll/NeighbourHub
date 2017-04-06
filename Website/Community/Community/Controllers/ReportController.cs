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
            ViewBag.Section = string.IsNullOrEmpty(section) ? "index" : section;
            ViewBag.PageNumber = pageNumber;
            ViewBag.ShowResolved = showResolved != null ? showResolved : false;

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
                reports = reports.Where(r => r.Resolved == false);
            }

            reports = reports.OrderBy(m => m.ID);

            return View(reports.ToPagedList(pageNumber, pageSize));
        }

        // GET: Report/Confirmation
        public ActionResult Confirmation()
        {
            return View();
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
                return RedirectToAction("Confirmation");
            }

            ViewBag.ReportedID = report.ReportedID;
            return View(report);
        }

        // GET: Report
        public ActionResult ReportUser(string userId)
        {
            var user = db.Users.Find(userId);
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

        // GET: Report/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ReportedEvent = new SelectList(db.Events, "ID", "HostID", report.ReportedEvent);
            ViewBag.ReportedID = new SelectList(db.Users, "ID", "Email", report.ReportedID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", report.UserID);
            return View(report);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ReportedEvent,ReportedID,Description")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportedEvent = new SelectList(db.Events, "ID", "HostID", report.ReportedEvent);
            ViewBag.ReportedID = new SelectList(db.Users, "ID", "Email", report.ReportedID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", report.UserID);
            return View(report);
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            return RedirectToAction("Index");
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
