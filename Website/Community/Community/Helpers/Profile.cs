using Community.Models;
using System;
using System.Linq;

namespace Community.Helpers
{
    public class ProfileHelper
    {
        /// <summary>
        /// Checks if current user has created a profile yet
        /// </summary>
        /// <returns>True if a profile exists, otherwise False</returns>
        public static bool ProfileExists(string userID)
        {
            CommunityEntities db = new CommunityEntities();
            var exists = db.Profiles.Where(p => p.UserID == userID).Any();
            return exists;
        }

        /// <summary>
        /// Finds the profile id of current users
        /// </summary>
        /// <returns></returns>
        public static int CurrentProfileID(string userID)
        {
            CommunityEntities db = new CommunityEntities();
            var profileID = db.Profiles
                .Where(p => p.UserID == userID)
                .Select(p => p.ID)
                .FirstOrDefault();

            return profileID;
        }

        ///<summary>Check whether profile is suspended</summary>
        ///<returns></returns>
        public static bool isSuspended(string userID) {
            CommunityEntities db = new CommunityEntities();
            var exists = db.Profiles
                .Where(p => p.UserID == userID
                    && p.Suspended == true)
                .Any();
            return exists;
        }

        ///<summary>Check whether profile is active</summary>
        ///<returns></returns>
        public static bool isActive(string userID)
        {
            CommunityEntities db = new CommunityEntities();
            var exists = db.Profiles
                .Where(p => p.UserID == userID
                    && p.Active == true)
                .Any();
            return exists;
        }

        public static bool canAffordVolunteer(string userID, int eventID)
        {
            CommunityEntities db = new CommunityEntities();
            int balance = getBalance(userID);
            int required = VolunteerHelper.getVolunteerPointValue(eventID);

            if (balance < required)
            {
                return false;
            }
            else {
                return true;
            }
        }

        public static int getBalance(string userID)
        {
            CommunityEntities db = new CommunityEntities();
            var profile = db.Profiles
                .Where(p => p.UserID == userID)
                .Select(p => new { Balance = p.Balance })
                .Single();
            return profile.Balance;
        }

        public static string getProfileImage(string userID)
        {
            CommunityEntities db = new CommunityEntities();
            var profile = db.Profiles
                .Where(p => p.UserID == userID)
                .Select(p => new { Picture = p.PictureURL })
                .Single();
            return profile.Picture;
        }
    }
}
