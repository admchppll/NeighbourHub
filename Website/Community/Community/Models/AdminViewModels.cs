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

    public class AdminReportMonth {
        public string Month { get; set; }
        public int Total { get; set; }
        public int Open { get; set; }
        public int Resolved { get; set; }
    }

    public class ReportPartialView {
        public int Past7Days { get; set; }
        public int Past14Days { get; set; }
        public int ThisMonth { get; set; }
        public int PreviousMonth { get; set; }
        public int ResolvedPast7Days { get; set; }
        public int ResolvedPast14Days { get; set; }
        public int ResolvedThisMonth { get; set; }
        public int ResolvedLastMonth { get; set; }
        public int TotalOpen { get; set; }
        public List<AdminReportMonth> Statistics {get;set;}
    }

    public class ContactPartialView {
        public int TotalOpen { get; set; }
        public List<Contact> Contacts { get; set;}
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
