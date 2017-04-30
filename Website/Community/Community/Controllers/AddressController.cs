using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;
using Community.Helpers;

namespace Community.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private CommunityEntities db = new CommunityEntities();

        public ActionResult UserPartial()
        {
            string userID = User.Identity.GetUserId();

            var addresses = db.Addresses
                .Where(a => a.UserID == userID)
                .OrderByDescending(a => a.Default)
                .ThenBy(a => a.Name);

            return View(addresses.ToList());
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address1,Address2,City,County,Country,Postcode,Notes,Default")] Address address)
        {
            address.UserID = User.Identity.GetUserId();
            if (Postcode.PostcodeIsValid(address.Postcode) == true)
            {
                Postcode postcode = new Postcode(address.Postcode);
                address.Long = postcode.longitude;
                address.Lat = postcode.latitude;

                if (ModelState.IsValid)
                {
                    db.Addresses.Add(address);
                    db.SaveChanges();
                    db.createGeoLocationAddress(address.ID);
                    if (address.Default == true)
                    {
                        AddressHelper.SetDefault(address.ID);
                    }
                    return RedirectToAction("Index", "Manage");
                }
            }
            else {
                ViewBag.PostcodeMessage = "Postcode is not valid";
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", address.UserID);
            return View(address);
        }

        public class AddressPostData {
            public int AddressID { get; set; }
        }

        //Handle MakeDefault requests
        public ActionResult MakeDefault(int? addressId) {
            if (addressId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(addressId);
            if (address == null)
            {
                return HttpNotFound();
            }
            AddressHelper.SetDefault(address.ID);
            return RedirectToAction("Index", "Manage");
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", address.UserID);
            return View(address);
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Name,Address1,Address2,City,County,Country,Postcode,Notes,Long,Lat,Default")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                if (address.Default == true)
                {
                    AddressHelper.SetDefault(address.ID);
                }
                return RedirectToAction("Index", "Manage");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Email", address.UserID);
            return View(address);
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = User.Identity.GetUserId();
            Address address = db.Addresses.Find(id);
            address.UserID = null;
            db.SaveChanges();

            AuditHelper.AddAudit(user, "Address Disassociated with User. Address ID: #" + address.ID);
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
