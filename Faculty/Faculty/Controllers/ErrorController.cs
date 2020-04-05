using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faculty.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult InternalError()
        {
            ViewBag.Error = "500 Congratulation, you broke the system :)";
            return View("Index");
        }
        public ActionResult NotFound()
        {
            ViewBag.Error = "404 not found :(";
            return View("Index");
        }

        public ActionResult Forbidden()
        {
            ViewBag.Error = "401 forbidden :(";
            return View("Index");
        }
    }
}