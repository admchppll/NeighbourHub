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
    public class ReviewController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Review
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Event).Include(r => r.User);
            return View(reviews.ToList());
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Review/Create
        public ActionResult Create(int volunteerId)
        {
            Volunteer volunteer = db.Volunteers.Find(volunteerId);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            string currentUser = User.Identity.GetUserId();

            ViewBag.VolunteerID = currentUser == volunteer.Event.HostID ? volunteer.VolunteerID : "";                         
            ViewBag.EventID = volunteer.EventID;
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,Rating,Review1,VolunteerID")] Review review)
        {
            string currentUser = User.Identity.GetUserId();
            review.UserID = currentUser;
            review.VolunteerID = string.IsNullOrEmpty(review.VolunteerID) ? null : review.VolunteerID;

            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Volunteer volunteer = db.Volunteers.Find(review.VolunteerID);

            ViewBag.VolunteerID = currentUser == volunteer.Event.HostID ? volunteer.VolunteerID : "";
            ViewBag.EventID = review.EventID;
            return View(review);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", review.EventID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", review.UserID);
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,EventID,Rating,Review1,VolunteerID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", review.EventID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", review.UserID);
            return View(review);
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
