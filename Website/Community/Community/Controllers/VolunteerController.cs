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
    public class VolunteerController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        public bool ProfileExists()
        {
            try
            {
                var userID = User.Identity.GetUserId();
                var count = db.Profiles
                    .Where(p => p.UserID == userID)
                    .Count();

                if (count == 0)
                    return false;
                else
                    return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        // GET: Volunteer
        public ActionResult Index()
        {
            var volunteers = db.Volunteers.Include(v => v.Event).Include(v => v.User);
            return View(volunteers.ToList());
        }

        // GET: Volunteer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer volunteer = db.Volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // GET: Volunteer/Create
        public ActionResult Create()
        {
            if (ProfileExists() == false)
            {
                return RedirectToAction("Create", "Profile");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID");
            ViewBag.VolunteerID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Volunteer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventID,VolunteerID,Confirmed,Rejected,Withdrawn")] Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Volunteers.Add(volunteer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", volunteer.EventID);
            ViewBag.VolunteerID = new SelectList(db.Users, "ID", "Email", volunteer.VolunteerID);
            return View(volunteer);
        }

        public class VolunteerPostData {
            public int Id { get; set; }
            public int EventId { get; set; }
        }

        //POST: Volunteer/Volunteer
        [HttpPost,ValidateHeaderAntiForgeryToken]
        public JsonResult Volunteer(VolunteerPostData data) {
            Volunteer volunteer = new Volunteer();
            volunteer.VolunteerID = User.Identity.GetUserId();
            volunteer.EventID = data.EventId;

            //Check if a volunteer already exists

            if (ModelState.IsValid) {
                db.Volunteers.Add(volunteer);
                try
                {
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                catch (Exception) { }                
            }
            return Json( new { success=false });
        }

        //POST: Volunteer/Confirm
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public bool Confirm(VolunteerPostData data)
        {
            var query = db.Volunteers
                    .Where(v => v.ID == data.Id);

            foreach (Volunteer vol in query) {
                if (vol.Event.HostID == User.Identity.GetUserId())
                {
                    vol.Confirmed = true;
                }
            }

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception) {}

            return false;
        }

        //POST: Volunteer/Reject
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public bool Reject(VolunteerPostData data)
        {
            var query = db.Volunteers
                .Include(v => v.Event)
                .Where(v => v.ID == data.Id);

            foreach (Volunteer vol in query)
            {
                if (vol.Event.HostID == User.Identity.GetUserId())
                {
                    vol.Rejected = true;
                }
            }

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception) { }

            return false;
        }

        //POST: Volunteer/Withdraw
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public bool Withdraw(VolunteerPostData data)
        {
            var query = db.Volunteers
                    .Where(v => v.ID == data.Id);

            foreach (Volunteer vol in query)
            {
                if (vol.VolunteerID == User.Identity.GetUserId())
                {
                    vol.Withdrawn = true;
                }
            }

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception) { }

            return false;
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
