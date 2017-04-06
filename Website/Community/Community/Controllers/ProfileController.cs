using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web;
using Community.Helpers;

namespace Community.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: Profile
        public ActionResult Index()
        {
            var profiles = db.Profiles.Include(p => p.User);
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
            string userID = User.Identity.GetUserId();
            if (ProfileHelper.ProfileExists(userID) == false)
            {
                ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
                return View();
            }
            else {
                return RedirectToAction("Edit", new { id = ProfileHelper.CurrentProfileID(userID) });
            }
        }

        // POST: Profile/Create
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Balance,Title,FirstName,Surname,Gender,BirthDate,Phone,Biography")] Profile profile, [Bind(Include = "PictureURL")]HttpPostedFileBase PictureURL)
        {
            DateTime current_date = DateTime.Now;
            if (PictureURL != null && PictureURL.ContentLength > 0)
            {
                string currentDateString = current_date.ToString("ddMMyy");
                Directory.CreateDirectory(Server.MapPath("~/Uploads/Profile/") + currentDateString);

                string fileExtension = Path.GetExtension(PictureURL.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Profile/" + currentDateString), fileName);
                PictureURL.SaveAs(filePath);

                profile.PictureURL = "~/Uploads/Profile/" + currentDateString + "/" + fileName;
            }
            profile.UserID = User.Identity.GetUserId();
            profile.Active = true;

            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return View(profile);
        }

        // POST: Profile/Edit/5
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Balance,Title,FirstName,Surname,Gender,BirthDate,Phone,Biography,PictureURL")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
