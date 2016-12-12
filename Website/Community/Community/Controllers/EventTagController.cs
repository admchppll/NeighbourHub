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
    public class EventTagController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: EventTag
        public ActionResult Index()
        {
            var eventTags = db.EventTags.Include(e => e.Event).Include(e => e.Tag);
            return View(eventTags.ToList());
        }

        // GET: EventTag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTag eventTag = db.EventTags.Find(id);
            if (eventTag == null)
            {
                return HttpNotFound();
            }
            return View(eventTag);
        }

        // GET: EventTag/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID");
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name");
            return View();
        }

        // POST: EventTag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventID,TagID")] EventTag eventTag)
        {
            if (ModelState.IsValid)
            {
                db.EventTags.Add(eventTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", eventTag.EventID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name", eventTag.TagID);
            return View(eventTag);
        }

        // GET: EventTag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTag eventTag = db.EventTags.Find(id);
            if (eventTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", eventTag.EventID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name", eventTag.TagID);
            return View(eventTag);
        }

        // POST: EventTag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventID,TagID")] EventTag eventTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", eventTag.EventID);
            ViewBag.TagID = new SelectList(db.Tags, "ID", "Name", eventTag.TagID);
            return View(eventTag);
        }

        // GET: EventTag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTag eventTag = db.EventTags.Find(id);
            if (eventTag == null)
            {
                return HttpNotFound();
            }
            return View(eventTag);
        }

        // POST: EventTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventTag eventTag = db.EventTags.Find(id);
            db.EventTags.Remove(eventTag);
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
