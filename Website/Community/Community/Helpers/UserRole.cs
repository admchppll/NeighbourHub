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
        public static void addToRole(string role, string userID) {
            using (CommunityEntities db = new CommunityEntities())
            {

                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.AddToRole(userID, role);
            }
        }
    }
}
