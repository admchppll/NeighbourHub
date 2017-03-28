using System.Web.Mvc;
using System.Web;
using System;
using System.Configuration;

namespace Community.Filters
{
    public class SetReturnUrl : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie UrlCookie = new HttpCookie("PreviousURL");
            UrlCookie.Value = filterContext.RequestContext.HttpContext.Request.UrlReferrer.ToString();
            UrlCookie.Expires = DateTime.Now.AddMinutes(10d);
            //UrlCookie.Domain = ConfigurationManager.AppSettings["Domain"];
            filterContext.HttpContext.Response.Cookies.Add(UrlCookie);
        }
    }
}
