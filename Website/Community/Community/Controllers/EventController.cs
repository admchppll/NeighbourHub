using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Spatial;
using Community.Helpers;
using System.Web;
using System.IO;
using PagedList;
using System.Linq.Expressions;
using Ganss.XSS;
using Community.Filters;
using System.Collections.Generic;

namespace Community.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private CommunityEntities db = new CommunityEntities();
        private const int pageSize = 15;
        private const double mile = 1609.34;

        [AllowAnonymous]
        // GET: Event
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);

            var events = db.Events
                .Include(y => y.Address)
                .Include(z => z.User)
                .OrderBy(e => e.ID);
            return View(events.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MyEvents(int? page)
        {
            int pageNumber = (page ?? 1);
            string userId = User.Identity.GetUserId();

            var events = db.Events
                .Include(y => y.Address)
                .Include(z => z.User)
                .Where(e => e.HostID == userId)
                .OrderBy(e => e.ID);
            return View(events.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public ActionResult Search(string postcode, double? distance, int? page)
        {
            int pageNumber = (page ?? 1);
            var resultRadius = distance * mile;

            if (postcode == "" || distance == null && page == null) {
                ViewBag.Search = false;
                return View();
            }

            ViewBag.Postcode = postcode;
            ViewBag.Distance = distance;
            ViewBag.Search = true;

            if (postcode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Postcode code = new Postcode(postcode);
            DbGeography geog = Postcode.CreateGeographyPoint(code.latitude, code.longitude);

            var events = db.EventSearches
                .Where(e => e.Published == true) 
                .Where(e => e.Suspended != true)
                .Where(e => e.VolunteerQuantity > e.Volunteers)
                .Where(e => e.Location.Distance(geog) < resultRadius)
                .Select(n => new EventSearchView () {
                    ID = n.ID,
                    Title = n.Title,
                    Description = n.Description,
                    Date = n.Date,
                    Time = n.Time,
                    Distance = (n.Location.Distance(geog)/mile) ?? 0,
                    Length = n.Length
                });
            events = events.OrderBy(e => e.Date);

            return View(events.ToPagedList(pageNumber, pageSize));
        }

        // GET: Event/List
        public ActionResult List(int? page)
        {
            int pageNumber = (page ?? 1);
            var user = User.Identity.GetUserId();

            var events = db.Events
                .Include(y => y.Address)
                .Include(z => z.User)
                .Where(e => e.HostID == user)
                .OrderBy(e => e.ID);
            return View(events.ToPagedList(pageNumber, pageSize));
        }


        [AllowAnonymous]
        // GET: Event/Details/5
        public ActionResult Details(int? id, float? distance)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int eventID = (int)id;

            Event @event = db.Events.Include(a => a.Address).Where(e => e.ID == eventID).SingleOrDefault();
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Distance = distance;
            return View(@event);
        }

        public ActionResult UserPartial()
        {
            string userID = User.Identity.GetUserId();

            var events = db.Events
                .Include(y => y.Address)
                .Where(e => e.HostID == userID && e.Date >= DateTime.Now)
                .OrderBy(e => e.Date)
                .Take(3);

            return View(events.ToList());
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();

            //Need to redirect to custom error page 
            //if (!ProfileHelper.isActive(userID)) {
            //    return HttpNotFound();
            //}

            if (ProfileHelper.ProfileExists(userID) == false)
            {
                return RedirectToAction("Create", "Profile", new { message = "You need to create a profile before you create an event!" });
            }

            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .Select(a => new {
                    AddressID = a.ID,
                    Label = a.Name + " (" + a.Address1 + ")" + (a.Default ? " DEFAULT" : ""),
                    Default = a.Default
                })
                .OrderByDescending(a => a.Default)
                .ToList();

            if (ProfileHelper.ProfileExists(userID) == false)
            {
                return RedirectToAction("Create", "Profile");
            }
            else if (AddressHelper.AddressExists(userID) == false){
                return RedirectToAction("Create", "Address");
            }

            ViewBag.AddressID = new SelectList(addresses, "AddressID", "Label");
            ViewBag.Tags = db.Tags.Where(v => v.Active == true);
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,ShortDescription,LongDescription,AddressID,Repeated,RepeatIncrement,Date,Length,AM1,AM2,AM3,AM4,AM5,AM6,AM7,PM1,PM2,PM3,PM4,PM5,PM6,PM7,DateInfo,VolunteerQuantity,Points")] Event @event, [Bind(Include="PictureURL")]HttpPostedFileBase PictureURL, [Bind(Include="Tags")]string Tags)
        {
            DateTime current_date = DateTime.Now;

            if (PictureURL != null && PictureURL.ContentLength > 0)
            {
                string currentDateString = current_date.ToString("ddMMyy");
                Directory.CreateDirectory(Server.MapPath("~/Uploads/Event/") + currentDateString);

                string fileExtension = Path.GetExtension(PictureURL.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Event/" + currentDateString), fileName);
                PictureURL.SaveAs(filePath);

                @event.PictureURL = "~/Uploads/Event/" + currentDateString + "/" + fileName;
            }

            @event.HostID = User.Identity.GetUserId();
            @event.Suspended = false;
            @event.Published = true;
            @event.Created = current_date;
            @event.Edited = current_date;

            var sanitizer = new HtmlSanitizer();
            @event.LongDescription = sanitizer.Sanitize(@event.LongDescription);
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();

                List<string> TagList = Tags.Split(',').ToList<string>();
                foreach (var t in TagList) {
                    int Tag = 0;
                    if (int.TryParse(t, out Tag))
                    {
                        EventTag et = new EventTag
                        {
                            EventID = @event.ID,
                            TagID = Tag
                        };
                        db.EventTags.Add(et);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            var userID = User.Identity.GetUserId();
            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .Select(a => new {
                    AddressID = a.ID,
                    Label = a.Name + " (" + a.Address1 + ")" + (a.Default ? " DEFAULT" : ""),
                    Default = a.Default
                })
                .OrderByDescending(a => a.Default)
                .ToList();

            ViewBag.AddressID = new SelectList(addresses, "AddressID", "Label");
            ViewBag.Tags = db.Tags.Where(v => v.Active == true);
            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            var userID = User.Identity.GetUserId();
            if (@event == null)
            {
                return HttpNotFound();
            }
            else if (UserHelper.IsAdmin(userID)) {
                return RedirectToAction("Details", new { id = id });
            }

            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .Select(a => new {
                    AddressID = a.ID,
                    Label = a.Name + " (" + a.Address1 + ")" + (a.Default ? " DEFAULT" : ""),
                    Default = a.Default
                })
                .OrderByDescending(a => a.Default)
                .ToList();

            ViewBag.AddressID = new SelectList(addresses, "AddressID", "Label");
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HostID,Title,ShortDescription,LongDescription,AddressID,Published,Repeated,RepeatIncrement,Created,Date,Time,Length,VolunteerQuantity,Points,AM1,AM2,AM3,AM4,AM5,AM6,AM7,PM1,PM2,PM3,PM4,PM5,PM6,PM7,DateInfo,PictureURL")] Event @event,[Bind(Include = "PictureURL1")]HttpPostedFileBase PictureURL1)
        {
            DateTime current_date = DateTime.Now;

            if (PictureURL1 != null && PictureURL1.ContentLength > 0)
            {
                string currentDateString = current_date.ToString("ddMMyy");
                Directory.CreateDirectory(Server.MapPath("~/Uploads/Event/") + currentDateString);

                string fileExtension = Path.GetExtension(PictureURL1.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Event/" + currentDateString), fileName);
                PictureURL1.SaveAs(filePath);

                @event.PictureURL = "~/Uploads/Event/" + currentDateString + "/" + fileName;
            }
            @event.Edited = current_date;
            var sanitizer = new HtmlSanitizer();
            @event.LongDescription = sanitizer.Sanitize(@event.LongDescription);

            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var userID = User.Identity.GetUserId();
            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .Select(a => new {
                    AddressID = a.ID,
                    Label = a.Name + " (" + a.Address1 + ")"
                })
                .ToList();

            ViewBag.AddressID = new SelectList(addresses, "AddressID", "Label");
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public class EventPostData {
            public int ID { get; set; }
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Suspend(EventPostData data)
        {
            Event @event = db.Events.Find(data.ID);

            if (@event.Suspended == true)
            {
                return Json(new
                {
                    success = false,
                    title = "Suspended! ",
                    message = "This event was already suspended!"
                });
            }

            @event.Suspended = true;
            @event.Published = false;

            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    title = "Success! ",
                    message = "This event has been marked as suspended!"
                });
            }

            return Json(new
            {
                success = false,
                title = "Something went wrong! ",
                message = "We couldn't suspend this event."
            });
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Restore(EventPostData data)
        {
            Event @event = db.Events.Find(data.ID);

            if (@event.Suspended == false)
            {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "This event is not suspended."
                });
            }

            @event.Suspended = false;
            @event.Published = true;

            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    title = "Success! ",
                    message = "This event has been restored"
                });
            }

            return Json(new
            {
                success = false,
                title = "Something went wrong! ",
                message = "We couldn't restore this event."
            });
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult Cancel(EventPostData data)
        {
            Event @event = db.Events.Find(data.ID);

            if (@event.Cancelled == true)
            {
                return Json(new
                {
                    success = false,
                    title = "Cancelled! ",
                    message = "This event was already cancelled!"
                });
            }

            @event.Cancelled = true;
            @event.Published = false;

            var volunteers = db.Volunteers.Where(v => v.EventID == data.ID).ToList();
            foreach (var v in volunteers) {
                v.Rejected = true;
                NotificationHelper.Create(v.VolunteerID, "Event Cancelled", "An event you have volunteered for has been cancelled.", String.Format("~/Event/Details/{0}", @event.ID));
            }

            var transactions = db.Transactions.Where(t => t.EventID == data.ID && t.Complete == false && t.Cancelled != true && t.ParentTransaction == null).ToList();
            foreach (var t in transactions) {
                db.reverseTransaction(t.ID);
            }

            AuditHelper.AddEventAudit(@event.ID, String.Format("Event Cancelled: #{0}", @event.ID), null);

            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    title = "Success! ",
                    message = "This event has been marked as cancelled!"
                });
            }

            return Json(new
            {
                success = false,
                title = "Something went wrong! ",
                message = "We couldn't cancel this event."
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
