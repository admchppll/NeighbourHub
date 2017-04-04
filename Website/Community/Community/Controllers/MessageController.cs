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
    public class MessageController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: Message
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            var messages = db.Messages
                .Include(m => m.User)
                .Include(m => m.User1)
                .Where(m => m.RecipientID == userID || m.SenderID == userID)
                .OrderByDescending(m => m.Sent);
            return View(messages.ToList());
        }

        // GET: Message/Read/5
        public ActionResult Read(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RecipientID,Title,Body,Saved,Admin")] Message message)
        {
            message.SenderID = User.Identity.GetUserId();
            message.Read = false;

            if (message.Saved == false)
            {
                message.Sent = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();

                if (message.Saved == false)
                {
                    message.Sent = DateTime.Now;
                    NotificationHelper.Create(
                        message.RecipientID, 
                        "New Message", 
                        "You have received a new message!", 
                        "~/Message/Read/" + message.ID);
                }

                return RedirectToAction("Index");
            }

            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            return View(message);
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            else if (message.Sent != null) {
                return View("Read", message);
            }
            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            return View(message);
        }

        // POST: Message/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SenderID,RecipientID,Title,Body,Saved,Admin")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            return View(message);
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
