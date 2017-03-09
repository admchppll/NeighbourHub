using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Community.Models;

namespace Community.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: Report
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Event).Include(r => r.User).Include(r => r.User1);
            return View(reports.ToList());
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,ReportedEvent,ReportedID,Description")] Report report)
        {
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
