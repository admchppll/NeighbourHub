using Community.Models;
using System.Linq;

namespace Community.Helpers
{
    public class NotificationHelper
    {
        public static void Create(string userID, string title, string description, string link) {
            Notification notification = new Notification();
            notification.UserID = userID;
            notification.Title = title;
            notification.Description = description;
            notification.Link = link;

            using (VolunteerEntities db = new VolunteerEntities()) {
                db.Notifications.Add(notification);
                db.SaveChanges();
            } 


        }

        //returns the number of unread notifications a user has
        public static int Unread(string userId) {
            VolunteerEntities db = new VolunteerEntities();
            var query = db.Notifications.Where(n => n.UserID == userId).Count();
            return query;
        }
    }
}
