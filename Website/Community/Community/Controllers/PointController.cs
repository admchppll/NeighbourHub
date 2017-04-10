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
    public class PointController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Point
        public ActionResult Index()
        {
            var points = db.Points.Include(p => p.Organisation).Include(p => p.User);
            return View(points.ToList());
        }

        // GET: Point/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // GET: Point/Create
        public ActionResult Create()
        {
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Point/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,OrganisationID,DayOfMonth,Expiry")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Points.Add(point);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", point.OrganisationID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", point.UserID);
            return View(point);
        }

        // GET: Point/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Points.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", point.OrganisationID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", point.UserID);
            return View(point);
        }

        // POST: Point/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,OrganisationID,DayOfMonth,Expiry")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Entry(point).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", point.OrganisationID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", point.UserID);
            return View(point);
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
