using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community.Controllers
{
    public class AdminController : Controller
    {
        CommunityEntities db = new CommunityEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactTile()
        {
            ContactPartialView model = new ContactPartialView();
            model.TotalOpen = db.Contacts.Where(c => c.LinkedEmail == null && c.Replied == false).Count();
            model.Contacts = db.Contacts.Where(c => c.LinkedEmail == null && c.Replied == false).OrderBy(c => c.Date).Take(3).ToList();
            return View(model);
        }

        public ActionResult ReportTile()
        {
            ReportPartialView model = new ReportPartialView();

            model.TotalUnresolved = db.Reports.Where(r => r.ResolvedDate == null).Count();

            var Statistics = db.ReportStatistics.OrderByDescending(r => r.Year).ThenByDescending(r => r.Month).Take(6).ToList();
            Statistics = Statistics.OrderBy(s => s.Year).ThenBy(s => s.Month).ToList();

            List<int> Reported = new List<int>();
            List<int> Resolved = new List<int>();
            List<int> Unresolved = new List<int>();
            List<string> Labels = new List<string>();

            foreach (var stat in Statistics)
            {
                Reported.Add(stat.ReportedCount);
                Resolved.Add(stat.ResolvedCount);
                Unresolved.Add(stat.UresolvedCount);
                Labels.Add(String.Format("{0} {1}", stat.MonthName, stat.Year));
            }

            model.Reported = string.Join(",", Reported);
            model.Resolved = string.Join(",", Resolved);
            model.Unresolved = string.Join(",", Unresolved);
            model.Labels = string.Join(",", Labels);

            return View(model);
        }

        public ActionResult EventTile()
        {
            EventPartialView model = new EventPartialView();

            model.Total = db.Events.Count();

            var Statistics = db.EventStatistics.OrderByDescending(r => r.Year).ThenByDescending(r => r.Month).Take(6).ToList();
            Statistics = Statistics.OrderBy(s => s.Year).ThenBy(s => s.Month).ToList();

            List<int> TotalCreated = new List<int>();
            List<int> TotalUnpublished = new List<int>();
            List<int> TotalCancelled = new List<int>();
            List<int> TotalSuspended = new List<int>();
            List<string> Labels = new List<string>();

            foreach (var stat in Statistics)
            {
                Labels.Add(String.Format("{0} {1}", stat.MonthName, stat.Year));
                TotalCreated.Add(stat.Total);
                TotalUnpublished.Add(stat.UnpublishedCount);
                TotalCancelled.Add(stat.CancelledCount);
                TotalSuspended.Add(stat.SuspendedCount);
            }

            model.Labels = string.Join(",", Labels);
            model.TotalCreated = string.Join(",", TotalCreated);
            model.TotalUnpublished = string.Join(",", TotalUnpublished);
            model.TotalCancelled = string.Join(",", TotalCancelled);
            model.TotalSuspended = string.Join(",", TotalSuspended);

            return View(model);
        }

        public ActionResult VolunteerTile()
        {
            VolunteerPartialView model = new VolunteerPartialView();

            model.Total = db.Events.Count();

            var Statistics = db.VolunteerStatistics.OrderByDescending(r => r.Year).ThenByDescending(r => r.Month).Take(6).ToList();
            Statistics = Statistics.OrderBy(s => s.Year).ThenBy(s => s.Month).ToList();

            List<int> PotentialVolunteers = new List<int>();
            List<int> VolunteerCount = new List<int>();
            List<int> WithdrawnCount = new List<int>();
            List<int> RejectedCount = new List<int>();
            List<int> ConfirmedCount = new List<int>();
            List<int> PendingConfirmationCount = new List<int>();
            List<string> Labels = new List<string>();

            foreach (var stat in Statistics)
            {
                Labels.Add(String.Format("{0} {1}", stat.MonthName, stat.Year));
                PotentialVolunteers.Add(stat.Potential);
                VolunteerCount.Add(stat.VolunteerCount);
                WithdrawnCount.Add(stat.WithdrawnCount);
                RejectedCount.Add(stat.RejectedCount);
                ConfirmedCount.Add(stat.ConfirmedCount);
                PendingConfirmationCount.Add(stat.PendingConfirmationCount);
            }

            model.Labels = string.Join(",", Labels);
            model.PotentialVolunteers = string.Join(",", PotentialVolunteers);
            model.VolunteerCount = string.Join(",", VolunteerCount);
            model.WithdrawnCount = string.Join(",", WithdrawnCount);
            model.RejectedCount = string.Join(",", RejectedCount);
            model.ConfirmedCount = string.Join(",", ConfirmedCount);
            model.PendingConfirmationCount = string.Join(",", PendingConfirmationCount);

            return View(model);
        }

        public ActionResult UserTile() {
            UserAdminView model = new UserAdminView();

            model.TotalUsers = db.Users.Count();
            model.TotalWithoutProfile = db.spUsersWithoutProfile().Single() ?? 0;
            model.TotalActiveUsers = db.Profiles.Where(p => p.Active == true && p.Suspended != true).Count();
            model.TotalInactiveUsers = db.Profiles.Where(p => p.Active == false && p.Suspended != true).Count();
            model.TotalSuspendedUsers = db.Profiles.Where(p => p.Suspended == true).Count();

            return View(model);
        }
    }
}
