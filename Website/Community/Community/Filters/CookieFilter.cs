using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Community.Filters
{
    public class CookiePermissionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["CookiePermission"];

            if (cookie == null)
            {
                HttpCookie permissionCookie = new HttpCookie("CookiePermission");
                permissionCookie.Value = "false";
                permissionCookie.Expires = DateTime.Now.AddYears(1);
                //permissionCookie.Domain = ConfigurationManager.AppSettings["Domain"];
                filterContext.HttpContext.Response.Cookies.Add(permissionCookie);
            }
        }
    }
}
