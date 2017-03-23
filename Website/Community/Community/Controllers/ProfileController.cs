using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;

namespace Community.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: Profile
        public ActionResult Index()
        {
            var profiles = db.Profiles.Include(p => p.Organisation).Include(p => p.User);
            return View(profiles.ToList());
        }

        [AllowAnonymous]
        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            if (ProfileExists() == false)
            {
                ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name");
                ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
                return View();
            }
            else {
                return RedirectToAction("Edit", new { id = CurrentProfileID() });
            }
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrganisationID,Balance,Title,FirstName,Surname,Gender,BirthDate,Phone,Biography,PictureURL,Active,Suspended")] Profile profile)
        {
            profile.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", profile.OrganisationID);
            return View(profile);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", profile.OrganisationID);
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,OrganisationID,Balance,Title,FirstName,Surname,Gender,BirthDate,Phone,Biography,PictureURL,Active,Suspended")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganisationID = new SelectList(db.Organisations, "ID", "Name", profile.OrganisationID);
            return View(profile);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Checks if current user has created a profile yet
        /// </summary>
        /// <returns>True if a profile exists, otherwise False</returns>
        public bool ProfileExists() {
            try
            {
                var userID = User.Identity.GetUserId();
                var count = db.Profiles.Where(p => p.UserID == userID).Count();

                if (count == 0)
                    return false;
                else
                    return true;
            }
            catch (NullReferenceException) {
                return false;
            }
        }

        /// <summary>
        /// Finds the profile id of current users
        /// </summary>
        /// <returns></returns>
        public int CurrentProfileID() {
            var userID = User.Identity.GetUserId();

            var profileID = db.Profiles
                .Where(p => p.UserID == userID)
                .Select(p => p.ID)
                .FirstOrDefault();

            return profileID;
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
