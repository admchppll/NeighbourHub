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
        public static int getUnresolved() {
            CommunityEntities db = new CommunityEntities();

            var count = db.Reports
                .Where(r => r.Resolved == false)
                .Count();
            return count;
        }

        public static int getUnresolvedEvent()
        {
            CommunityEntities db = new CommunityEntities();

            var count = db.Reports
                .Where(r => r.ReportedEvent != null 
                    && r.Resolved == false)
                .Count();
            return count;
        }

        public static int getUnresolvedUser()
        {
            CommunityEntities db = new CommunityEntities();

            var count = db.Reports
                .Where(r => r.ReportedID != null
                    && r.Resolved == false)
                .Count();
            return count;
        }
    }
}
