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

        public ActionResult Volunteers(int eventId)
        {
            ViewBag.EventID = eventId;
            var volunteers = db.Volunteers
                .Include(v => v.Event)
                .Include(v => v.User)
                .Where(v => v.EventID == eventId)
                .OrderBy(v => v.Accepted)
                .OrderBy(v => v.Confirmed)
                .OrderBy(v => v.Rejected)
                .OrderBy(v => v.Withdrawn);
            return PartialView(volunteers.ToList());
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
            bool volunteerExists = VolunteerHelper.isVolunteer(volunteer.VolunteerID, volunteer.EventID);

            if (volunteerExists) {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "It appears you have already volunteered on this event."
                });
            }
            else if (ModelState.IsValid) {
                db.Volunteers.Add(volunteer);
                try
                {
                    db.SaveChanges();
                    return Json(new {
                        success = true,
                        title = "Thank You! ",
                        message = "You have been added as a volunteer. You will be notified when the host accepts you on the event!"
                    });
                }
                catch (Exception) { }                
            }
            return Json( new {
                success = false,
                title = "",
                message = "We were unable to add you as a volunteer! Please refresh the page and try again! If the problem continues, please contact us."
            });
        }

        //POST: Volunteer/Confirm
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Confirm(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool volunteerExists = VolunteerHelper.existsVolunteer(data.Id);

            if (volunteerExists == false)
            {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "We couldn't find the volunteer to confirm, please refresh the page and try again! If the problem continues, please contact us."
                });
            }
            else {
                Volunteer volunteer = db.Volunteers.Find(data.Id);

                if (VolunteerHelper.isHost(userID, data.EventId))
                {
                    try
                    {
                        volunteer.Confirmed = true;
                        db.Entry(volunteer).State = EntityState.Modified;
                        db.SaveChanges();

                        return Json(new
                        {
                            success = true,
                            title = "",
                            message = "This volunteer's attendance has been confirmed."
                        });
                    }
                    catch (Exception) { }
                }
                else {
                    return Json(new
                    {
                        success = false,
                        title = "",
                        message = "It looks like this isn't your event. You cannot confirm a volunteer for an event you are not the host of."
                    });
                }
            }

            return Json(new
                {
                    success = false,
                    title = "",
                    message = "We were unable to confirm this volunteer! Please refresh the page and try again! If the problem continues, please contact us."
                });
        }

        //POST: Volunteer/Accept
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Accept(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool volunteerExists = VolunteerHelper.existsVolunteer(data.Id);

            if (volunteerExists == false)
            {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "We couldn't find the volunteer to accept, please refresh the page and try again! If the problem continues, please contact us."
                });
            }
            else {
                Volunteer volunteer = db.Volunteers.Find(data.Id);

                if (VolunteerHelper.isHost(userID, data.EventId))
                {
                    try
                    {
                        volunteer.Accepted = true;
                        db.Entry(volunteer).State = EntityState.Modified;
                        db.SaveChanges();

                        return Json(new
                        {
                            success = true,
                            title = "",
                            message = "This volunteer has been accepted."
                        });
                    }
                    catch (Exception) { }
                }
                else {
                    return Json(new
                    {
                        success = false,
                        title = "",
                        message = "It looks like this isn't your event. You cannot accept a volunteer for an event you are not the host of."
                    });
                }
            }

            return Json(new
            {
                success = false,
                title = "",
                message = "We were unable to accept this volunteer! Please refresh the page and try again! If the problem continues, please contact us."
            });
        }

        //POST: Volunteer/Reject
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Reject(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool volunteerExists = VolunteerHelper.existsVolunteer(data.Id);

            if (volunteerExists == false)
            {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "We couldn't find the volunteer to reject, please refresh the page and try again! If the problem continues, please contact us."
                });
            }
            else {
                Volunteer volunteer = db.Volunteers.Find(data.Id);

                if (VolunteerHelper.isHost(userID, data.EventId))
                {
                    try
                    {
                        volunteer.Rejected = true;
                        db.Entry(volunteer).State = EntityState.Modified;
                        db.SaveChanges();

                        return Json(new
                        {
                            success = true,
                            title = "",
                            message = "This volunteer has been rejected."
                        });
                    }
                    catch (Exception) { }
                }
                else {
                    return Json(new
                    {
                        success = false,
                        title = "",
                        message = "It looks like this isn't your event. You cannot reject a volunteer for an event you are not the host of."
                    });
                }
            }

            return Json(new
            {
                success = false,
                title = "",
                message = "We were unable to reject this volunteer! Please refresh the page and try again! If the problem continues, please contact us."
            });
        }

        //POST: Volunteer/Withdraw
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Withdraw(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool isVolunteer = VolunteerHelper.isVolunteer(userID, data.EventId);

            if (isVolunteer == false)
            {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "It appears you haven't volunteered yet!"
                });
            }
            else {
                int volunteerID = VolunteerHelper.getVolunteer(userID, data.EventId);
                Volunteer volunteer = db.Volunteers.Find(volunteerID);

                try
                {
                    volunteer.Withdrawn = true;
                    db.Entry(volunteer).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        title = "Sorry you don't want to volunteer anymore. ",
                        message = "You have been successfully withdrawn from this event."
                    });
                }
                catch (Exception) { }
            }

            return Json(new
            {
                success = false,
                title = "",
                message = "We were unable to withdraw you from this event! Please refresh the page and try again! If the problem continues, please contact us."
            });
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
