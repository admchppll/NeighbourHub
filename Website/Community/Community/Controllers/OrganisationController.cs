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
using Community.Helpers;

namespace Community.Controllers
{
    [Authorize]
    public class OrganisationController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Organisation
        public ActionResult Index()
        {
            return View(db.Organisations.ToList());
        }

        // GET: Organisation/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisations.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }
            return View(organisation);
        }

        // GET: Organisation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organisation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Email,Phone,Address1,Address2,City,County,Country,Postcode,Facebook,Twitter,Google,Youtube,CharityNumber")] Organisation organisation)
        {
            organisation.Balance = 0;
            organisation.Approved = false;
            organisation.Active = false;

            UserOrganisation uo = new UserOrganisation();
            uo.UserID = User.Identity.GetUserId();
            uo.Approved = false;
            uo.Denied = false;
            uo.Admin = true;
            uo.Created = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Organisations.Add(organisation);
                db.SaveChanges();
                uo.OrganisationID = organisation.ID;
                db.UserOrganisations.Add(uo);
                db.SaveChanges();

                AuditHelper.AddAudit(
                    uo.UserID,
                    "Organisation Created: #" + organisation.ID + " " + organisation.Name);

                return RedirectToAction("Index", "Manage");
            }

            return View(organisation);
        }

        public ActionResult Approve(short? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisations.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }
            return View(organisation);
        }

        [HttpPost, ActionName("Approve")]
        public ActionResult Approve(short id) {

            Organisation organisation = db.Organisations.Find(id);
            organisation.Approved = true;
            organisation.Active = true;

            UserOrganisation uo = db.UserOrganisations.Where(u => u.OrganisationID == id).Single();
            uo.Approved = true;

            if (ModelState.IsValid) {
                db.Entry(organisation).State = EntityState.Modified;
                db.Entry(uo).State = EntityState.Modified;
                db.SaveChanges();
            }

            UserRoleHelper.addToRole("OrganisationAdmin", uo.UserID);

            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult Deactivate(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisations.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }
            return View(organisation);
        }

        [HttpPost, ActionName("Deactivate")]
        public ActionResult Deactivate(short id)
        {

            Organisation organisation = db.Organisations.Find(id);
            organisation.Active = false;

            if (ModelState.IsValid)
            {
                db.Entry(organisation).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Details", new { id = id });
        }

        // GET: Organisation/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisations.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }
            return View(organisation);
        }

        // POST: Organisation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Balance,Email,Phone,Address1,Address2,City,County,Country,Postcode,Facebook,Twitter,Google,Youtube,CharityNumber,Approved")] Organisation organisation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organisation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organisation);
        }

        // GET: Organisation/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organisation organisation = db.Organisations.Find(id);
            if (organisation == null)
            {
                return HttpNotFound();
            }
            return View(organisation);
        }

        // POST: Organisation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Organisation organisation = db.Organisations.Find(id);
            db.Organisations.Remove(organisation);
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
