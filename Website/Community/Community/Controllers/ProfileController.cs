using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web;
using Community.Helpers;
using PagedList;

namespace Community.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private CommunityEntities db = new CommunityEntities();
        private const int pageSize = 20;

        // GET: Profile
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page, string prefix)
        {
            int pageNumber = (page ?? 1);
            var profiles = db.Profiles
                .Include(p => p.User)
                .Where(p => p.FirstName.Contains(prefix) || p.Surname.Contains(prefix) || p.User.UserName.Contains(prefix))
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.Surname)
                .ThenBy(p => p.User.UserName);

            ViewBag.TotalResults = profiles.Count();
            return View(profiles.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentInts = String.Join(",", db.UserInterests.Include(i => i.Interest).Where(ui => ui.UserID == profile.UserID).Select(i => i.Interest.Label).ToArray());
            ViewBag.CurrentSkills = String.Join(",", db.UserSkills.Include(s => s.Skill1).Where(ui => ui.UserID == profile.UserID).Select(s => s.Skill1.Label).ToArray());
            return View(profile);
        }

        [AllowAnonymous]
        public ActionResult EventPartial(string userId)
        {
            Profile profile = db.Profiles.Where(p => p.UserID == userId).Single();
            return View(profile);
        }

        public ActionResult UserPartial()
        {
            string userID = User.Identity.GetUserId();
            int profileID = ProfileHelper.CurrentProfileID(userID);

            var profile = db.Profiles.Find(profileID);

            return View(profile);
        }

        // GET: Profile/Create
        public ActionResult Create(string message = "")
        {
            string userID = User.Identity.GetUserId();
            if (ProfileHelper.ProfileExists(userID) == false)
            {
                ViewBag.Message = message != ""? message : null;
                ViewBag.Interests = db.Interests;
                ViewBag.Skills = db.Skills.Where(s => s.Active == true);

                return View();
            }
            else {
                return RedirectToAction("Edit", new { id = ProfileHelper.CurrentProfileID(userID) });
            }
        }

        // POST: Profile/Create
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ID,Balance,Title,FirstName,Surname,Gender,BirthDate,Phone,Biography")] Profile profile, 
            [Bind(Include = "PictureURL")]HttpPostedFileBase PictureURL, 
            [Bind(Include = "Interests")]String Interests, 
            [Bind(Include = "Skills")]String Skills)
        {
            DateTime current_date = DateTime.Now;
            if (PictureURL != null && PictureURL.ContentLength > 0)
            {
                string currentDateString = current_date.ToString("ddMMyy");
                Directory.CreateDirectory(Server.MapPath("~/Uploads/Profile/") + currentDateString);

                string fileExtension = Path.GetExtension(PictureURL.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Profile/" + currentDateString), fileName);
                PictureURL.SaveAs(filePath);

                profile.PictureURL = "~/Uploads/Profile/" + currentDateString + "/" + fileName;
            }
            profile.UserID = User.Identity.GetUserId();
            profile.Active = true;

            var interestList = Interests.Split(',').ToList();
            var skillList = Skills.Split(',').ToList();

            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();

                foreach (var id in interestList) {
                    UserInterest ui = new UserInterest();
                    ui.InterestID = Convert.ToInt32(id);
                    ui.UserID = profile.UserID;
                    db.UserInterests.Add(ui);
                }

                foreach (var id in skillList) {
                    UserSkill us = new UserSkill();
                    us.Skill = Convert.ToInt32(id);
                    us.UserID = profile.UserID;
                    db.UserSkills.Add(us);
                }
                db.SaveChanges();

                return RedirectToAction("Index", "Manage");
            }
            ViewBag.Interests = db.Interests;
            ViewBag.Skills = db.Skills.Where(s => s.Active == true);
            return View(profile);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }

            ViewBag.InterestSelected = db.UserInterests.Include(i => i.Interest).Where(ui => ui.UserID == profile.UserID);
            ViewBag.CurrentInts = String.Join(",", db.UserInterests.Where(ui => ui.UserID == profile.UserID).Select(i => i.InterestID).ToArray());
            ViewBag.SkillSelected = db.UserSkills.Include(s => s.Skill1).Where(ui => ui.UserID == profile.UserID);
            ViewBag.CurrentSkills = String.Join(",", db.UserSkills.Where(ui => ui.UserID == profile.UserID).Select(s => s.Skill).ToArray());
            ViewBag.Interests = db.Interests;
            ViewBag.Skills = db.Skills.Where(s => s.Active == true);

            return View(profile);
        }

        // POST: Profile/Edit/5
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ID,UserID,Balance,Title,FirstName,Surname,Gender,BirthDate,Phone,Biography, PictureURL")] Profile profile, 
            [Bind(Include = "PictureURL1")]HttpPostedFileBase PictureURL1, 
            [Bind(Include = "Interests")]String Interests, 
            [Bind(Include = "Skills")]String Skills, 
            [Bind(Include = "DeletedInterests")]String DeletedInterests, 
            [Bind(Include = "DeletedSkills")]String DeletedSkills)
        {
            DateTime current_date = DateTime.Now;
            if (PictureURL1 != null && PictureURL1.ContentLength > 0)
            {
                string currentDateString = current_date.ToString("ddMMyy");
                Directory.CreateDirectory(Server.MapPath("~/Uploads/Profile/") + currentDateString);

                string fileExtension = Path.GetExtension(PictureURL1.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Profile/" + currentDateString), fileName);
                PictureURL1.SaveAs(filePath);

                profile.PictureURL = "~/Uploads/Profile/" + currentDateString + "/" + fileName;
            }

            var interestList = Interests.Split(',').ToList();
            var skillList = Skills.Split(',').ToList();
            var delInterestList = DeletedInterests.Split(',').ToList();
            var delSkillList = DeletedSkills.Split(',').ToList();

            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;

                foreach (var id in delInterestList) {
                    if (String.IsNullOrEmpty(id)) {
                        continue;
                    }
                    int delId = Convert.ToInt32(id);
                    if (db.UserInterests.Where(u => u.InterestID == delId && u.UserID == profile.UserID).Any())
                    {
                        UserInterest del = db.UserInterests.Where(u => u.InterestID == delId && u.UserID == profile.UserID).Single();
                        db.UserInterests.Remove(del);
                    }
                }

                foreach (var id in interestList)
                {
                    if (String.IsNullOrEmpty(id))
                    {
                        continue;
                    }
                    int intId = Convert.ToInt32(id);
                    if (!db.UserInterests.Where(u => u.InterestID == intId && u.UserID == profile.UserID).Any()) {
                        UserInterest ui = new UserInterest();
                        ui.InterestID = intId;
                        ui.UserID = profile.UserID;
                        db.UserInterests.Add(ui);
                    }
                }

                foreach (var id in delSkillList)
                {
                    if (String.IsNullOrEmpty(id))
                    {
                        continue;
                    }
                    int delId = Convert.ToInt32(id);
                    if (db.UserSkills.Where(u => u.Skill == delId && u.UserID == profile.UserID).Any())
                    {
                        UserSkill del = db.UserSkills.Where(u => u.Skill == delId && u.UserID == profile.UserID).Single();
                        db.UserSkills.Remove(del);
                    }
                }

                foreach (var id in skillList)
                {
                    if (String.IsNullOrEmpty(id))
                    {
                        continue;
                    }
                    int skId = Convert.ToInt32(id);
                    if (!db.UserSkills.Where(u => u.Skill == skId && u.UserID == profile.UserID).Any())
                    {
                        UserSkill us = new UserSkill();
                        us.Skill = skId;
                        us.UserID = profile.UserID;
                        db.UserSkills.Add(us);
                    }                      
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            ViewBag.InterestSelected = db.UserInterests.Include(i => i.Interest).Where(ui => ui.UserID == profile.UserID);
            ViewBag.CurrentInts = String.Join(",",db.UserInterests.Where(ui => ui.UserID == profile.UserID).Select(i => i.InterestID).ToArray());
            ViewBag.SkillSelected = db.UserSkills.Include(s => s.Skill1).Where(ui => ui.UserID == profile.UserID);
            ViewBag.CurrentSkills = String.Join(",", db.UserSkills.Where(ui => ui.UserID == profile.UserID).Select(s => s.Skill).ToArray());
            ViewBag.Interests = db.Interests;
            ViewBag.Skills = db.Skills.Where(s => s.Active == true);
            return View(profile);
        }

        public class ProfilePostData
        {
            public string ID { get; set; }
        }

        [HttpPost, ValidateHeaderAntiForgeryToken, Authorize(Roles = "Admin")]
        public JsonResult Suspend(ProfilePostData data)
        {
            int profileId = ProfileHelper.CurrentProfileID(data.ID);
            Profile profile = db.Profiles.Find(profileId);

            if (profile.Suspended == true)
            {
                return Json(new
                {
                    success = false,
                    title = "Suspended! ",
                    message = "This profile was already suspended!"
                });
            }

            profile.Suspended = true;

            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    title = "Success! ",
                    message = "This profile has been suspended!"
                });
            }

            return Json(new
            {
                success = false,
                title = "Something went wrong! ",
                message = "We couldn't suspend this profile."
            });
        }

        [HttpPost, ValidateHeaderAntiForgeryToken, Authorize(Roles = "Admin")]
        public JsonResult Restore(ProfilePostData data)
        {
            int profileId = ProfileHelper.CurrentProfileID(data.ID);
            Profile profile = db.Profiles.Find(profileId);

            if (profile.Suspended == false)
            {
                return Json(new
                {
                    success = false,
                    title = "",
                    message = "This profile is not suspended."
                });
            }

            profile.Suspended = false;

            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    title = "Success! ",
                    message = "This profile has been removed from suspension!"
                });
            }

            return Json(new
            {
                success = false,
                title = "Something went wrong! ",
                message = "This profile could not be restored."
            });
        }

        [HttpPost]
        public JsonResult ProfileList(string prefix) {
            var users = db.Users
                .Where(u => u.UserName.Contains(prefix))
                .OrderBy(u => u.UserName)
                .Select(u => new {
                    ID = u.ID,
                    UserName = u.UserName })
                .Take(10)
                .ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
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
