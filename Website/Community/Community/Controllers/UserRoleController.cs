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
    public class UserRoleController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        // GET: UserRole
        public ActionResult Index()
        {
            var userRoles = db.UserRoles.Include(u => u.Role).Include(u => u.User);
            return View(userRoles.ToList());
        }

        // GET: UserRole/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            ViewBag.IdentityUser_Id = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: UserRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId")] UserRole userRole)
        {
            userRole.IdentityUser_Id = userRole.UserId;
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", userRole.RoleId);
            ViewBag.IdentityUser_Id = new SelectList(db.Users, "ID", "Email", userRole.IdentityUser_Id);
            return View(userRole);
        }

        // GET: UserRole/Delete/5
        public ActionResult Delete(string userId, string roleId)
        {
            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(roleId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Where(ur => ur.RoleId == roleId & ur.UserId == userId).Single();
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string userId, string roleId)
        {
            UserRole userRole = db.UserRoles.Where(ur => ur.RoleId == roleId & ur.UserId == userId).Single();
            db.UserRoles.Remove(userRole);
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
