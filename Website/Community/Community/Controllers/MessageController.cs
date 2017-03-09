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
    [Authorize]
    public class MessageController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: Message
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.User).Include(m => m.User1);
            return View(messages.ToList());
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.SenderID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SenderID,RecipientID,Title,Body,Sent,Read,Saved,Notified,Admin")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            ViewBag.SenderID = new SelectList(db.Users, "ID", "Email", message.SenderID);
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
            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            ViewBag.SenderID = new SelectList(db.Users, "ID", "Email", message.SenderID);
            return View(message);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SenderID,RecipientID,Title,Body,Sent,Read,Saved,Notified,Admin")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipientID = new SelectList(db.Users, "ID", "Email", message.RecipientID);
            ViewBag.SenderID = new SelectList(db.Users, "ID", "Email", message.SenderID);
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
