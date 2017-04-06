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
    public class UserOrganisationController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: UserOrganisation
        public ActionResult Index()
        {
            var userOrganisations = db.UserOrganisations.Include(u => u.Organisation).Include(u => u.User);
            return View(userOrganisations.ToList());
        }

        // GET: UserOrganisation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserOrganisation userOrganisation = db.UserOrganisations.Find(id);
            if (userOrganisation == null)
            {
                return HttpNotFound();
            }
            return View(userOrganisation);
        }

        // GET: UserOrganisation/Create
        public ActionResult Create()
        {
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: UserOrganisation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,OrganisationID,Approved,Denied,Admin,Created")] UserOrganisation userOrganisation)
        {
            if (ModelState.IsValid)
            {
                db.UserOrganisations.Add(userOrganisation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", userOrganisation.OrganisationID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userOrganisation.UserID);
            return View(userOrganisation);
        }

        // GET: UserOrganisation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserOrganisation userOrganisation = db.UserOrganisations.Find(id);
            if (userOrganisation == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", userOrganisation.OrganisationID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userOrganisation.UserID);
            return View(userOrganisation);
        }

        // POST: UserOrganisation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,OrganisationID,Approved,Denied,Admin,Created")] UserOrganisation userOrganisation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userOrganisation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", userOrganisation.OrganisationID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userOrganisation.UserID);
            return View(userOrganisation);
        }

        // GET: UserOrganisation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserOrganisation userOrganisation = db.UserOrganisations.Find(id);
            if (userOrganisation == null)
            {
                return HttpNotFound();
            }
            return View(userOrganisation);
        }

        // POST: UserOrganisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserOrganisation userOrganisation = db.UserOrganisations.Find(id);
            db.UserOrganisations.Remove(userOrganisation);
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
