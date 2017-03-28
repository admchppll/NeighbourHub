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
    public class BookmarkController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: Bookmark
        public ActionResult Index()
        {
            var bookmarkeds = db.Bookmarkeds.Include(b => b.Event).Include(b => b.User);
            return View(bookmarkeds.ToList());
        }

        // GET: Bookmark/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmarked bookmarked = db.Bookmarkeds.Find(id);
            if (bookmarked == null)
            {
                return HttpNotFound();
            }
            return View(bookmarked);
        }

        // GET: Bookmark/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Bookmark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventID,UserID")] Bookmarked bookmarked)
        {
            if (ModelState.IsValid)
            {
                db.Bookmarkeds.Add(bookmarked);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", bookmarked.EventID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", bookmarked.UserID);
            return View(bookmarked);
        }

        // GET: Bookmark/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmarked bookmarked = db.Bookmarkeds.Find(id);
            if (bookmarked == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", bookmarked.EventID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", bookmarked.UserID);
            return View(bookmarked);
        }

        // POST: Bookmark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventID,UserID")] Bookmarked bookmarked)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookmarked).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", bookmarked.EventID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", bookmarked.UserID);
            return View(bookmarked);
        }

        // GET: Bookmark/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmarked bookmarked = db.Bookmarkeds.Find(id);
            if (bookmarked == null)
            {
                return HttpNotFound();
            }
            return View(bookmarked);
        }

        // POST: Bookmark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookmarked bookmarked = db.Bookmarkeds.Find(id);
            db.Bookmarkeds.Remove(bookmarked);
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
