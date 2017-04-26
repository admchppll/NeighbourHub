using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community.Controllers
{
    /// <summary>
    /// Controller to return custom error pages
    /// </summary>
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult NotPermitted() {
            return View();
        }

        public ActionResult InternalError()
        {
            return View();
        }

        public ActionResult Timeout()
        {
            return View();
        }

        public ActionResult Unavailable()
        {
            return View();
        }
    }
}