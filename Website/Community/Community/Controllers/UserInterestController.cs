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
    public class UserInterestController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: UserInterest
        public ActionResult Index()
        {
            var userInterests = db.UserInterests.Include(u => u.Interest).Include(u => u.User);
            return View(userInterests.ToList());
        }

        // GET: UserInterest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInterest userInterest = db.UserInterests.Find(id);
            if (userInterest == null)
            {
                return HttpNotFound();
            }
            return View(userInterest);
        }

        // GET: UserInterest/Create
        public ActionResult Create()
        {
            ViewBag.InterestID = new SelectList(db.Interests, "ID", "Label");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: UserInterest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,InterestID")] UserInterest userInterest)
        {
            if (ModelState.IsValid)
            {
                db.UserInterests.Add(userInterest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InterestID = new SelectList(db.Interests, "ID", "Label", userInterest.InterestID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userInterest.UserID);
            return View(userInterest);
        }

        // GET: UserInterest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInterest userInterest = db.UserInterests.Find(id);
            if (userInterest == null)
            {
                return HttpNotFound();
            }
            ViewBag.InterestID = new SelectList(db.Interests, "ID", "Label", userInterest.InterestID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userInterest.UserID);
            return View(userInterest);
        }

        // POST: UserInterest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,InterestID")] UserInterest userInterest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInterest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InterestID = new SelectList(db.Interests, "ID", "Label", userInterest.InterestID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userInterest.UserID);
            return View(userInterest);
        }

        // GET: UserInterest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInterest userInterest = db.UserInterests.Find(id);
            if (userInterest == null)
            {
                return HttpNotFound();
            }
            return View(userInterest);
        }

        // POST: UserInterest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInterest userInterest = db.UserInterests.Find(id);
            db.UserInterests.Remove(userInterest);
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
