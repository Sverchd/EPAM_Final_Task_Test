using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using Faculty.Filters;

namespace Faculty.Controllers
{
    [ExceptionFilter]
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IThemeService _themeService;
        private readonly IUserService _userService;

        public HomeController(ICourseService courseService, IThemeService themeService, IUserService userService)
        {
            _courseService = courseService;
            _themeService = themeService;
            _userService = userService;
        }

        public ActionResult Index()
        {


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