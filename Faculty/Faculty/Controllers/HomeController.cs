using System.Web.Mvc;
using BusinessLogicLayer;
using BusinessLogicLayer.Contracts;

namespace Faculty.Controllers
{
    public class HomeController : Controller
    {
        private ICourseService _courseService;
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