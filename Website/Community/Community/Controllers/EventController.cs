﻿using System;
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

namespace Community.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();  

        [AllowAnonymous]
        // GET: Event
        public ActionResult Index()
        {
            var events = db.Events.Include(y => y.Address).Include(z => z.User);
            return View(events.ToList());
        }

        [AllowAnonymous]
        public ActionResult Search() {
            ViewBag.Search = false;
            return View();
        }

        [AllowAnonymous, HttpPost]
        // POST: Event
        public ActionResult Search(string postcode, double distance)
        {
            const double mile = 1609.34;
            var resultRadius = distance * mile;

            ViewBag.Postcode = postcode;
            ViewBag.Distance = distance;
            ViewBag.Search = true;

            if (postcode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Postcode code = new Postcode(postcode);
            DbGeography geog = Postcode.CreateGeographyPoint(code.latitude, code.longitude);

            var events = db.Events
                .Include(y => y.Address)
                .Include(z => z.User)
                .Where(y => y.Address.GeoLocation.Distance(geog) < resultRadius)
                .Where(y => y.Suspended != true && y.Published == true);
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
            int eventID = (int)id;

            Event @event = db.Events.Where(e => e.ID == eventID).SingleOrDefault();
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

            if (Helpers.ProfileHelper.isActive(userID)) {
                return HttpNotFound();
            }

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
