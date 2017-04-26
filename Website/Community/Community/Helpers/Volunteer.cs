using Community.Models;
using System;
using System.Linq;

namespace Community.Helpers
{
    public class VolunteerHelper
    {
        /// <summary>
        /// Check if volunteer id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ExistsVolunteer(int id) {
            CommunityEntities db = new CommunityEntities();

            bool exists = db.Volunteers
                .Where(v => v.ID == id)
                .Any();

            return exists;
        }

        /// <summary>
        /// Check if user has already volunteered on the event 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static bool IsVolunteer(string userId, int eventID) {
            CommunityEntities db = new CommunityEntities();

            bool exists = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userId)
                .Any();
            
            return exists;
        }

        /// <summary>
        /// Check if current user is approved(accepted) on the current event 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static bool IsApprovedVolunteer(string userId, int eventID)
        {
            CommunityEntities db = new CommunityEntities();

            bool exists = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userId
                        && v.Accepted == true
                        && v.Rejected != true
                        && v.Withdrawn != true)
                .Any();

            return exists;
        }

        /// <summary>
        /// Checks if current user has been withdrawn from given event id 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static bool IsWithdrawn(string userID, int eventID)
        {
            CommunityEntities db = new CommunityEntities();

            bool exists = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userID
                        && v.Withdrawn == true)
                .Any();

            return exists;
        }

        /// <summary>
        /// Checks if current user is confirmed on given event id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static bool IsConfirmed(string userID, int eventID)
        {
            CommunityEntities db = new CommunityEntities();

            bool exists = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userID
                        && v.Confirmed == true)
                .Any();

            return exists;
        }

        /// <summary>
        /// Get the volunteer id relative to the user and event given
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static int GetVolunteer(string userID, int eventID)
        {
            CommunityEntities db = new CommunityEntities();

            var volunteer = db.Volunteers
                .Where(v => v.EventID == eventID
                        && v.VolunteerID == userID).SingleOrDefault();

            return volunteer.ID;
        }

        /// <summary>
        /// Returns host id of an event
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static string GetHost(int eventID) {
            CommunityEntities db = new CommunityEntities();

            var @event = db.Events.Find(eventID);

            return @event.HostID;
        }

        /// <summary>
        /// Checks whether given user is host of the event provided
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static bool IsHost(string hostID, int eventID)
        {
            CommunityEntities db = new CommunityEntities();

            bool exists = db.Events
                .Where(v => v.ID == eventID
                        && v.HostID == hostID)
                .Any();

            return exists;
        }

        /// <summary>
        /// Get the value of points allocated per volunteer on event
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static short GetVolunteerPointValue(int eventID)
        {
            CommunityEntities db = new CommunityEntities();
            var @event = db.Events
                .Where(e => e.ID == eventID)
                .Select(e => new {
                    Value = e.Points,
                    Quantity = e.VolunteerQuantity
                })
                .Single();

            short result = Convert.ToInt16(@event.Value / @event.Quantity);
            return result;
        }

        /// <summary>
        /// Checks whether event has filled all requested positions
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static bool IsEventFull(int eventID) {
            CommunityEntities db = new CommunityEntities();

            int volunteersRequested = db.Events.Find(eventID).VolunteerQuantity;
            //All accepted and not withdrawn/rejected
            int volunteerCount = db.Volunteers
                .Where(v => v.EventID == eventID 
                    && v.Accepted == true 
                    && v.Rejected == false 
                    && v.Withdrawn == false)
                .Count();

            if (volunteersRequested > volunteerCount)
            {
                return false;
            }
            else {
                return true;
            }
        }
        
        /// <summary>
        /// Returns the number of volunteers on an event
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static int CountVolunteers(int eventID)
        {
            CommunityEntities db = new CommunityEntities();

            //All accepted and not withdrawn/rejected
            int volunteerCount = db.Volunteers
                .Where(v => v.EventID == eventID
                    && v.Accepted == true
                    && v.Rejected == false
                    && v.Withdrawn == false)
                .Count();

            return volunteerCount;
        }
    }
}
