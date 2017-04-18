using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Helpers
{
    public class ReportHelper
    {
        public static int GetUnresolved() {
            CommunityEntities db = new CommunityEntities();

            var count = db.Reports
                .Where(r => r.ResolvedDate == null)
                .Count();
            return count;
        }

        public static int GetUnresolvedEvent()
        {
            CommunityEntities db = new CommunityEntities();

            var count = db.Reports
                .Where(r => r.ReportedEvent != null 
                    && r.ResolvedDate == null)
                .Count();
            return count;
        }

        public static int GetUnresolvedUser()
        {
            CommunityEntities db = new CommunityEntities();

            var count = db.Reports
                .Where(r => r.ReportedID != null
                    && r.ResolvedDate == null)
                .Count();
            return count;
        }
    }
}
