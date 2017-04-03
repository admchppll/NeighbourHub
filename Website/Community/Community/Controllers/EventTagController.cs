using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Community.Models;

namespace Community.Controllers
{
    public class EventTagController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        public ActionResult Display(int? eventID)
        {
            var eventTags = db.EventTags
                .Include(e => e.Tag)
                .Where(e => e.EventID == eventID);
            return PartialView(eventTags.ToList());
        }

        public ActionResult Utility(int? eventID)
        {
            var eventTags = db.EventTags
                .Include(e => e.Tag)
                .Where(e => e.EventID == eventID);

            ViewBag.Tags = db.Tags.Where(v => v.Active == true);
            ViewBag.EventID = eventID;

            return PartialView(eventTags.ToList());
        }

        // GET: EventTag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTag eventTag = db.EventTags.Find(id);
            if (eventTag == null)
            {
                return HttpNotFound();
            }
            return View(eventTag);
        }

        public class EventTagPostData {
            public int EventID { get; set; }
            public int TagID { get; set; }
        }

        // POST: EventTag/Create
        [HttpPost,ValidateHeaderAntiForgeryToken]
        public JsonResult Create(EventTagPostData data)
        {
            EventTag eventTag = new EventTag();
            eventTag.EventID = data.EventID;
            eventTag.TagID = data.TagID;


            if (ModelState.IsValid)
            {
                db.EventTags.Add(eventTag);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new
                {
                    success = true,
                    id = eventTag.ID
                });
            }

            return Json(new { success = false });
        }

        // POST: EventTag/Delete/5
        [HttpPost,ValidateHeaderAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            EventTag eventTag = db.EventTags.Find(id);
            db.EventTags.Remove(eventTag);
            db.SaveChanges();
            return Json(new { success = true });
            //return RedirectToAction("Index");
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
