using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Community.Controllers
{
    /***************************************************************************************
    * Title: ValidateHeaderAntiForgeryToken.cs
    * Author: Harms, Joshua
    * Date: 2014
    * Code version: 1.0
    * Availability: https://gist.github.com/nozzlegear/eb3e4560580fc21f2032
    * Purpose: Validate tokens contained within headers of custom post requests where previous built-in functions are no longer satisfactory
    **************************************************************************************/
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateHeaderAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
    
            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);
        }
    }
}
