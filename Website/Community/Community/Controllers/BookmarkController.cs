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
    public class BookmarkController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Bookmark
        public ActionResult Index()
        {
            var bookmarkeds = db.Bookmarks.Include(b => b.Event).Include(b => b.User);
            return View(bookmarkeds.ToList());
        }

        // GET: Bookmark/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Bookmark/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventID,UserID")] Bookmark bookmarked)
        {
            if (ModelState.IsValid)
            {
                db.Bookmarks.Add(bookmarked);
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
            Bookmark bookmarked = db.Bookmarks.Find(id);
            if (bookmarked == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", bookmarked.EventID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", bookmarked.UserID);
            return View(bookmarked);
        }

        // POST: Bookmark/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventID,UserID")] Bookmark bookmarked)
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
            Bookmark bookmarked = db.Bookmarks.Find(id);
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
            Bookmark bookmarked = db.Bookmarks.Find(id);
            db.Bookmarks.Remove(bookmarked);
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
