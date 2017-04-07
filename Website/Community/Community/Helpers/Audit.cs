using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Helpers
{
    public class AuditHelper
    {
        public static void addEventAudit(int eventId, string message, int report) {
            CommunityEntities db = new CommunityEntities();
            var e = db.Events.Find(eventId);

            Audit audit = new Audit();
            audit.Date = DateTime.Now;
            audit.UserID = e.HostID;
            audit.EventID = eventId;
            audit.AuditMessage = message;
            audit.ReportID = report;

            db.Audits.Add(audit);
            db.SaveChanges();
        }

        public static void addUserAudit(string userId, string message, int report)
        {
            CommunityEntities db = new CommunityEntities();

            Audit audit = new Audit();
            audit.Date = DateTime.Now;
            audit.UserID = userId;
            audit.AuditMessage = message;
            audit.ReportID = report;

            db.Audits.Add(audit);
            db.SaveChanges();
        }

        public static void addAudit(int eventId, string message)
        {
            CommunityEntities db = new CommunityEntities();
            var e = db.Events.Find(eventId);

            Audit audit = new Audit();
            audit.Date = DateTime.Now;
            audit.UserID = e.HostID;
            audit.EventID = eventId;
            audit.AuditMessage = message;

            db.Audits.Add(audit);
            db.SaveChanges();
        }
    }
}

