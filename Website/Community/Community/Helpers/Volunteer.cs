using Community.Models;
using System.Linq;

namespace Community.Helpers
{
    public class VolunteerHelper
    {
        public static bool existsVolunteer(int id) {
            VolunteerEntities db = new VolunteerEntities();

            bool exists = db.Volunteers
                .Where(v => v.ID == id)
                .Any();

            return exists;
        }

        public static bool isVolunteer(string userId, int eventID) {
            VolunteerEntities db = new VolunteerEntities();

            bool exists = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userId)
                .Any();
            
            return exists;
        }

        public static bool isWithdrawn(string userID, int eventID)
        {
            VolunteerEntities db = new VolunteerEntities();

            bool exists = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userID
                        && v.Withdrawn == true)
                .Any();

            return exists;
        }

        public static int getVolunteer(string userID, int eventID)
        {
            VolunteerEntities db = new VolunteerEntities();

            var volunteer = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userID).SingleOrDefault();

            return volunteer.ID;
        }

        public static string getHost(int eventID) {
            VolunteerEntities db = new VolunteerEntities();

            var @event = db.Events.Find(eventID);

            return @event.HostID;
        }

        public static bool isHost(string hostID, int eventID)
        {
            VolunteerEntities db = new VolunteerEntities();

            bool exists = db.Events
                .Where(v => v.ID == eventID
                        && v.HostID == hostID)
                .Any();

            return exists;
        }
    }
}
