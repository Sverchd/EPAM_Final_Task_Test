using System;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using Faculty.Filters;

namespace Faculty.Controllers
{
    [ExceptionFilter]
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;

        public HomeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public ActionResult Index()
        {
            _courseService.GetAllCourses();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}