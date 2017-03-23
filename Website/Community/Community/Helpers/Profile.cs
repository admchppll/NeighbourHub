using Community.Models;
using System;
using System.Linq;

namespace Community.Helpers
{
    public class Profile
    {
        private VolunteerEntities db = new VolunteerEntities();

        /// <summary>
        /// Checks if current user has created a profile yet
        /// </summary>
        /// <returns>True if a profile exists, otherwise False</returns>
        public bool ProfileExists(string userID)
        {
            try
            {
                var count = db.Profiles.Where(p => p.UserID == userID).Count();

                if (count == 0)
                    return false;
                else
                    return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Finds the profile id of current users
        /// </summary>
        /// <returns></returns>
        public int CurrentProfileID(string userID)
        {
            var profileID = db.Profiles
                .Where(p => p.UserID == userID)
                .Select(p => p.ID)
                .FirstOrDefault();

            return profileID;
        }

        ///<summary>Check whether profile is suspended/active</summary>
        ///<returns></returns>
    }
}
