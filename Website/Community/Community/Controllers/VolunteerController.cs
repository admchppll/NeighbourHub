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
        private CommunityEntities db = new CommunityEntities();

        // GET: Volunteer
        public ActionResult Index()
        {
            var volunteers = db.Volunteers
                .Include(v => v.Event)
                .Include(v => v.User);
            return View(volunteers.ToList());
        }

        /// <summary>
        /// Screen used on event details page
        /// </summary>
        /// <param name="eventId"></param>
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

        /// <summary>
        /// Screen used on users hub  /Manage/Index
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPartial()
        {
            string userID = User.Identity.GetUserId();

            var events = db.Volunteers
                .Include(e => e.Event)
                .Include(y => y.Event.Address)
                .Where(v => v.VolunteerID == userID)
                .Where(v => v.Event.Date >= DateTime.Now)
                .OrderBy(v => v.Event.Date)
                .Take(3);

            return View(events.ToList());
        }

        public class VolunteerPostData {
            public int Id { get; set; }
            public int EventId { get; set; }
        }

        //POST: Volunteer/Volunteer (Used in AJAX)
        [HttpPost,ValidateHeaderAntiForgeryToken]
        public JsonResult Volunteer(VolunteerPostData data) {
            Volunteer volunteer = new Volunteer();
            volunteer.VolunteerID = User.Identity.GetUserId();
            volunteer.EventID = data.EventId;

            //Check if a volunteer already exists
            bool volunteerExists = VolunteerHelper.IsVolunteer(volunteer.VolunteerID, volunteer.EventID);

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

        //POST: Volunteer/Confirm (Used in AJAX)
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Confirm(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool volunteerExists = VolunteerHelper.ExistsVolunteer(data.Id);

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

                if (VolunteerHelper.IsHost(userID, data.EventId))
                {
                    try
                    {
                        volunteer.Confirmed = true;
                        db.Entry(volunteer).State = EntityState.Modified;
                        db.SaveChanges();
                        db.confirmVolunteer(data.Id);

                        NotificationHelper.Create(
                            volunteer.VolunteerID,
                            "Confirmed",
                            "You have been confirmed as attending an event",
                            "~/Event/Details/" + volunteer.EventID);

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

        //POST: Volunteer/Accept (Used in AJAX)
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Accept(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool volunteerExists = VolunteerHelper.ExistsVolunteer(data.Id);

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
                if (ProfileHelper.CanAffordVolunteer(userID, data.EventId) == false) {
                    return Json(new
                    {
                        success = false,
                        title = "",
                        message = "We couldn't accept the volunteer, unfortunately you don't have enough tokens to host this volunteer! Try volunteering for more events to get more tokens!"
                    });
                }
                else if (VolunteerHelper.IsHost(userID, data.EventId))
                {
                    try
                    {
                        Volunteer volunteer = db.Volunteers.Find(data.Id);
                        volunteer.Accepted = true;

                        //Create transaction to put tokens on hold for volunteer
                        TransactionHelper.CreateEventTrans(
                            userID, 
                            volunteer.VolunteerID,
                            volunteer.EventID, 
                            VolunteerHelper.GetVolunteerPointValue(volunteer.EventID));
                        
                        db.Entry(volunteer).State = EntityState.Modified;
                        db.SaveChanges();

                        NotificationHelper.Create(
                            volunteer.VolunteerID,
                            "Accepted",
                            "You have been accepted as a volunteer",
                            "~/Event/Details/" + volunteer.EventID);


                        //Reject All volunteers when total volunteers asked for are filled
                        if (VolunteerHelper.IsEventFull(data.EventId)) {
                            var volunteers = db.Volunteers.Where(v => v.EventID == data.EventId && v.Accepted != true && v.Confirmed != true && v.Rejected != true && v.Withdrawn != true).ToList();
                            foreach (var v in volunteers)
                            {
                                v.Rejected = true;
                                NotificationHelper.Create(v.VolunteerID, "Event Full", "An event you have volunteered for has been filled.", String.Format("~/Event/Details/{0}", v.EventID));
                            }
                            db.SaveChanges();
                        }

                        return Json(new
                        {
                            success = true,
                            title = "",
                            message = "This volunteer has been accepted."
                        });
                    }
                    catch (Exception) {
                        return Json(new
                        {
                            success = false,
                            title = "",
                            message = "We were unable to accept this volunteer! Please refresh the page and try again! If the problem continues, please contact us."
                        });
                    }
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
        }

        //POST: Volunteer/Reject (Used in AJAX)
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Reject(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool volunteerExists = VolunteerHelper.ExistsVolunteer(data.Id);

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
                if (VolunteerHelper.IsHost(userID, data.EventId))
                {
                    try
                    {
                        Volunteer volunteer = db.Volunteers.Find(data.Id);
                        volunteer.Rejected = true;

                        db.Entry(volunteer).State = EntityState.Modified;
                        db.SaveChanges();

                        string title;
                        string message;

                        if (volunteer.Accepted == true)
                        {
                            db.reverseTransaction(TransactionHelper.GetVolunteerTransactionID(data.EventId, volunteer.VolunteerID, userID));

                            title = "Did not attend!";
                            message = "You have been marked as not attending an event";
                        }
                        else {
                            title = "Rejected";
                            message = "You have been rejected as a volunteer";
                        }
                        //Notify volunteer of rejection
                        NotificationHelper.Create(
                            volunteer.VolunteerID,
                            title,
                            message,
                            "~/Event/Details/" + volunteer.EventID);

                        if (volunteer.Accepted == false)
                        {
                            return Json(new
                            {
                                success = true,
                                title = "",
                                message = "This volunteer has been rejected."
                            });
                        }
                        else {
                            return Json(new
                            {
                                success = true,
                                title = "",
                                message = "This volunteer has been successfully marked as not attending."
                            });
                        }
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

        //POST: Volunteer/Withdraw (Used in AJAX)
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Withdraw(VolunteerPostData data)
        {
            string userID = User.Identity.GetUserId();
            bool isVolunteer = VolunteerHelper.IsVolunteer(userID, data.EventId);

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
                int volunteerID = VolunteerHelper.GetVolunteer(userID, data.EventId);
                Volunteer volunteer = db.Volunteers.Find(volunteerID);

                try
                {
                    volunteer.Withdrawn = true;
                    db.Entry(volunteer).State = EntityState.Modified;
                    db.SaveChanges();

                    NotificationHelper.Create(
                            VolunteerHelper.GetHost(data.EventId),
                            "Withdrawn",
                            "A volunteer has withdrawn from your event!",
                            "~/Event/Details/" + data.EventId);

                    if (volunteer.Accepted) {
                        db.reverseTransaction(TransactionHelper.GetVolunteerTransactionID(data.EventId, volunteer.VolunteerID, userID));
                    }
                      
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
