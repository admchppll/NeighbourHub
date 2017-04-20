using Community.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Helpers
{
    public class UserRoleHelper
    {
        public static void AddToRole(string role, string userID) {
            using (CommunityEntities db = new CommunityEntities())
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.AddToRole(userID, role);
            }
        }
    }

    public class UserHelper {
        public static int ExternalLogins(string userID) {
            using (CommunityEntities db = new CommunityEntities())
            {

                int count = db.UserLogins.Where(u => u.UserId == userID).Count();
                return count;
            }
        }

        public static bool IsAdmin(string userID) {
            using (CommunityEntities db = new CommunityEntities())
            {
                var role = db.Roles.Where(r => r.Name == "Admin").Single();
                bool count = db.UserRoles.Where(u => u.UserId == userID && u.RoleId == role.Id).Any();
                return count;
            }
        }

        public static bool IsConnectedToOrganisation(string userID)
        {
            CommunityEntities db = new CommunityEntities();

            var exists = db.UserOrganisations.Where(u => u.UserID == userID).Any();

            return exists;
        }

        public static string GetUsername(string userID) {
            using (CommunityEntities db = new CommunityEntities())
            {
                var user = db.Users.Find(userID);
                return user.UserName;
            }
        }
    }
}
