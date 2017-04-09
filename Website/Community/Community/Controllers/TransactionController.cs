using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Community.Models;
using Microsoft.AspNet.Identity;
using System;
using PagedList;

namespace Community.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private CommunityEntities db = new CommunityEntities();
        private const int pageSize = 15;

        // GET: Transaction
        public ActionResult Index(int? page, string sortOrder)
        {
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = string.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewBag.SenderSortParm = sortOrder == "Sender" ? "sender_desc" : "Sender";
            ViewBag.ReceiveSortParm = sortOrder == "Receive" ? "receive_desc" : "Receive";

            var transactions = db.Transactions
                .Include(t => t.User)
                .Include(t => t.User1)
                .Include(t => t.Event);

            switch (sortOrder)
            {
                case "Receive":
                    transactions = transactions.OrderBy(t => t.User.UserName).ThenByDescending(t => t.Date);
                    break;
                case "receive_desc":
                    transactions = transactions.OrderByDescending(t => t.User.UserName).ThenByDescending(t => t.Date);
                    break;
                case "Sender":
                    transactions = transactions.OrderBy(t => t.User1.UserName).ThenByDescending(t => t.Date);
                    break;
                case "sender_desc":
                    transactions = transactions.OrderByDescending(t => t.User1.UserName).ThenByDescending(t => t.Date);
                    break;
                case "Amount":
                    transactions = transactions.OrderBy(t => t.Amount).ThenByDescending(t => t.Date);
                    break;
                case "amount_desc":
                    transactions = transactions.OrderByDescending(t => t.Amount).ThenByDescending(t => t.Date);
                    break;
                case "date_asc":
                    transactions = transactions.OrderBy(t => t.Date);
                    break;
                default:
                    transactions = transactions.OrderByDescending(t => t.Date);
                    break;
            }

            return View(transactions.ToPagedList(pageNumber, pageSize));
        }

        // GET: Transaction/UserTransaction/5
        public ActionResult UserTransactions(int? page, string section, string sortOrder)
        {
            int pageNumber = (page ?? 1);
            ViewBag.Section = string.IsNullOrEmpty(section) ? "index" : section;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = string.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "amount_desc" : "Amount";

            var userID = User.Identity.GetUserId();
            var transactions = db.Transactions
                .Include(t => t.User)
                .Include(t => t.User1)
                .Include(t => t.Event);

            switch (section)
            {
                case "sent":
                    transactions = transactions.Where(t => t.SenderID == userID);
                    break;
                case "received":
                    transactions = transactions.Where(t => t.RecipientID == userID);
                    break;
                default:
                    transactions = transactions.Where(t => t.RecipientID == userID || t.SenderID == userID);
                    break;
            }

            switch (sortOrder)
            {
                case "Amount":
                    transactions = transactions.OrderBy(t => t.Amount).ThenByDescending(t => t.Date);
                    break;
                case "amount_desc":
                    transactions = transactions.OrderByDescending(t => t.Amount).ThenByDescending(t => t.Date);
                    break;
                case "date_asc":
                    transactions = transactions.OrderBy(t => t.Date);
                    break;
                default:
                    transactions = transactions.OrderByDescending(t => t.Date);
                    break;
            }


            return View(transactions.ToPagedList(pageNumber, pageSize));
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID");
            return View();
        }

        // POST: Transaction/Create
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SenderID,RecipientID,EventID,Gift,Amount,Complete,Cancelled")] Transaction transaction)
        {
            transaction.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", transaction.EventID);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", transaction.EventID);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,SenderID,RecipientID,EventID,Gift,Amount,Complete")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "ID", "HostID", transaction.EventID);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
