using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faculty.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Action for 500 error
        /// </summary>
        /// <returns>error view</returns>
        public ActionResult InternalError()
        {
            ViewBag.Error = "500 Congratulation, you broke the system :)";
            return View("Index");
        }
        /// <summary>
        /// Action for 404 error
        /// </summary>
        /// <returns>error view</returns>
        public ActionResult NotFound()
        {
            ViewBag.Error = "404 not found :(";
            return View("Index");
        }
        /// <summary>
        /// Action for 401 error
        /// </summary>
        /// <returns>error view</returns>
        public ActionResult Forbidden()
        {
            ViewBag.Error = "401 forbidden :(";
            return View("Index");
        }
    }
}