using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Community.Models
{
    public class AdminIndex
    {
        public string UserID { get; set; }
    }

    public class ReportPartialView {
        public int TotalUnresolved { get; set; }
        public string Labels { get; set; }
        public string Reported { get; set; }
        public string Resolved { get; set; }
        public string Unresolved { get; set; }
    }

    public class ContactPartialView {
        public int TotalOpen { get; set; }
        public List<Contact> Contacts { get; set;}
    }

    public class EventPartialView {
        public string Labels { get; set; }
        public string TotalCreated { get; set; }
        public string TotalUnpublished { get; set; }
        public string TotalCancelled { get; set; }
        public string TotalSuspended { get; set; }
        public int Total { get; set; }
    }

    public class VolunteerPartialView
    {
        public string Labels { get; set; }
        public string PotentialVolunteers { get; set; }
        public string VolunteerCount { get; set; }
        public string WithdrawnCount { get; set; }
        public string RejectedCount { get; set; }
        public string ConfirmedCount { get; set; }
        public string PendingConfirmationCount { get; set; }
        public int Total { get; set; }
    }

    public class UserAdminView {
        public int TotalUsers { get; set; }
        public int TotalWithoutProfile { get; set; }
        public int TotalActiveUsers { get; set; }
        public int TotalInactiveUsers { get; set; }
        public int TotalSuspendedUsers { get; set; }
    }

    public class ContactAdminView {
        public int Total { get; set; }
        public int TotalOpen { get; set; }
        public List<Contact> Contacts { get; set; }
    }

    public class ContactDisplayModel {
        public Contact Contact { get; set; }
        public List<Contact> Replies { get; set; }
    }

    public class ContactReply {
        public int ContactID { get; set; }
        [AllowHtml]
        public string Message { get; set; }
    }
}
