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
    public class UserSkillController : Controller
    {
        private VolunteerEntities db = new VolunteerEntities();

        // GET: UserSkill
        public ActionResult Index()
        {
            var userSkills = db.UserSkills.Include(u => u.Skill1).Include(u => u.User);
            return View(userSkills.ToList());
        }

        // GET: UserSkill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSkill userSkill = db.UserSkills.Find(id);
            if (userSkill == null)
            {
                return HttpNotFound();
            }
            return View(userSkill);
        }

        // GET: UserSkill/Create
        public ActionResult Create()
        {
            ViewBag.Skill = new SelectList(db.Skills, "ID", "Label");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: UserSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Skill")] UserSkill userSkill)
        {
            if (ModelState.IsValid)
            {
                db.UserSkills.Add(userSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Skill = new SelectList(db.Skills, "ID", "Label", userSkill.Skill);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userSkill.UserID);
            return View(userSkill);
        }

        // GET: UserSkill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSkill userSkill = db.UserSkills.Find(id);
            if (userSkill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Skill = new SelectList(db.Skills, "ID", "Label", userSkill.Skill);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userSkill.UserID);
            return View(userSkill);
        }

        // POST: UserSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Skill")] UserSkill userSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Skill = new SelectList(db.Skills, "ID", "Label", userSkill.Skill);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", userSkill.UserID);
            return View(userSkill);
        }

        // GET: UserSkill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSkill userSkill = db.UserSkills.Find(id);
            if (userSkill == null)
            {
                return HttpNotFound();
            }
            return View(userSkill);
        }

        // POST: UserSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSkill userSkill = db.UserSkills.Find(id);
            db.UserSkills.Remove(userSkill);
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
