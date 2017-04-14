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
                .Where(e => e.Location.Distance(geog) < resultRadius);

            events = events.OrderBy(e => e.Date);

            return View(events.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int eventID = (int)id;

            Event @event = db.Events.Where(e => e.ID == eventID).SingleOrDefault();
            if (@event == null)
            {
                return HttpNotFound();
            }
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

            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .Select(a => new {
                    AddressID = a.ID,
                    Label = a.Name + " (" + a.Address1 + ")"
                })
                .ToList();

            if (ProfileHelper.ProfileExists(userID) == false)
            {
                return RedirectToAction("Create", "Profile");
            }
            else if (AddressHelper.AddressExists(userID) == false){
                return RedirectToAction("Create", "Address");
            }

            ViewBag.AddressID = new SelectList(addresses, "AddressID", "Label");
            ViewBag.Tags = new SelectList(db.Tags.Where(v => v.Active == true),"ID", "Name");
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,ShortDescription,LongDescription,AddressID,Repeated,RepeatIncrement,Date,Length,AM1,AM2,AM3,AM4,AM5,AM6,AM7,PM1,PM2,PM3,PM4,PM5,PM6,PM7,DateInfo,VolunteerQuantity,Points")] Event @event, [Bind(Include="PictureURL")]HttpPostedFileBase PictureURL)
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

            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
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

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
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
