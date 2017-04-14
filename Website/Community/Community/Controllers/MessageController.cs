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
using PagedList;

namespace Community.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private CommunityEntities db = new CommunityEntities();
        private const int pageSize = 15;
        // GET: Message
        public ActionResult Index(string section, string sortOrder, int? page)
        {
            int pageNumber = (page ?? 1);
            string userID = User.Identity.GetUserId();

            ViewBag.Section = String.IsNullOrEmpty(section) ? "index" : section;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SentSortParm = String.IsNullOrEmpty(sortOrder) ? "sent_asc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            var messages = db.Messages
                .Include(m => m.User)
                .Include(m => m.User1);

            switch (section) {
                case "sent":
                    messages = messages.Where(m => m.Sent != null && m.SenderID == userID);
                    break;
                case "received":
                    messages = messages.Where(m => m.Sent != null && m.RecipientID == userID);
                    break;
                case "saved":
                    messages = messages.Where(m => m.Sent == null && m.SenderID == userID);
                    break;
                default:
                    messages = messages.Where(m => (m.RecipientID == userID && m.Sent != null) || m.SenderID == userID);
                    break;
            }

            switch (sortOrder)
            {
                case "Read":
                    messages = messages.OrderBy(m => m.Read);
                    break;
                case "read_desc":
                    messages = messages.OrderByDescending(m => m.Read);
                    break;
                case "Title":
                    messages = messages.OrderBy(m => m.Title);
                    break;
                case "title_desc":
                    messages = messages.OrderByDescending(m => m.Title);
                    break;
                case "sent_asc":
                    messages = messages.OrderBy(m => m.Sent);
                    break;
                default:  
                    messages = messages.OrderByDescending(m => m.Sent);
                    break;
            }

            return View(messages.ToPagedList(pageNumber, pageSize));
        }

        // GET: Message
        public ActionResult Admin()
        {
            string userID = User.Identity.GetUserId();
            var messages = db.Messages
                .Include(m => m.User)
                .Include(m => m.User1)
                .Where(m => m.Admin == true)
                .OrderByDescending(m => m.Sent);
            return View(messages.ToList());
        }

        public ActionResult UserPartial()
        {
            string userID = User.Identity.GetUserId();

            var messages = db.Messages
                .Include(m => m.User)
                .Include(m => m.User1)
                .Where(m => m.Sent != null && m.RecipientID == userID)
                .OrderByDescending(m => m.Sent)
                .Take(3);
            return PartialView(messages.ToList());
        }

        // GET: Message/Read/5
        public ActionResult Read(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            Message message = db.Messages.Find(id);

            //if not the user who sent/received or message doesn't exist redirect
            if (message == null
                || (message.SenderID != userId && message.RecipientID != userId))
            {
                return HttpNotFound();
            } else if (message.RecipientID == userId 
                && message.Read == false) {
                MessageHelper.setRead(userId, message.ID);
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
        public ActionResult Create([Bind(Include = "ID,RecipientID,Title,Body,Saved,Admin,ParentMessage")] Message message)
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

        // GET: Message/Reply
        public ActionResult Reply(int? messageID)
        {
            Message message = db.Messages.Find(messageID);
            ViewBag.RecipientID = message.SenderID;
            ViewBag.Title = "RE: " + message.Title;
            ViewBag.ParentID = message.ID;
            return PartialView("Reply");
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
            if (message.Sent == null) {
                db.Messages.Remove(message);
                db.SaveChanges();
            }  
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
