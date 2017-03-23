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
    public class EventController : Controller
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

        public bool AddressExists()
        {
            try
            {
                var userID = User.Identity.GetUserId();
                var count = db.Addresses.Where(p => p.UserID == userID).Count();

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

        [AllowAnonymous]
        // GET: Event
        public ActionResult Index()
        {
            var events = db.Events.Include(y => y.Address);
            return View(events.ToList());
        }

        [AllowAnonymous]
        // GET: Event/Details/5
        public ActionResult Details(int? id)
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

        // GET: Event/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();
            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .Select(a => new {
                    AddressID = a.ID,
                    Label = a.Name + " (" + a.Address1 + ")"
                })
                .ToList();

            if (ProfileExists() == false)
            {
                return RedirectToAction("Create", "Profile");
            }
            else if (AddressExists() == false){
                return RedirectToAction("Create", "Address");
            }

            ViewBag.AddressID = new SelectList(addresses, "AddressID", "Label");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ShortDescription,LongDescription,AddressID,Repeated,RepeatIncrement,Date,Length,AM1,AM2,AM3,AM4,AM5,AM6,AM7,PM1,PM2,PM3,PM4,PM5,PM6,PM7,DateInfo")] Event @event)
        {
            DateTime current_date = DateTime.Now;

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

            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "Name", @event.AddressID);
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
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "Name", @event.AddressID);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HostID,Title,ShortDescription,LongDescription,AddressID,Published,Repeated,RepeatIncrement,Date,Length,AM1,AM2,AM3,AM4,AM5,AM6,AM7,PM1,PM2,PM3,PM4,PM5,PM6,PM7,DateInfo,Suspended")] Event @event)
        {
            DateTime current_date = DateTime.Now;

            @event.HostID = User.Identity.GetUserId();
            @event.Edited = current_date;

            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "ID", "UserID", @event.AddressID);
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
