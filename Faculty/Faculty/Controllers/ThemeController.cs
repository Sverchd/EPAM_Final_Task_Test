using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Faculty.Models;

namespace Faculty.Controllers
{
    public class ThemeController : Controller
    {
        // GET: Theme
        private readonly IThemeService _themeService;
        private readonly ICourseService _courseService;
        public ThemeController()
        {

        }

        public ThemeController(IThemeService themeService, ICourseService courseService)
        {
            _themeService = themeService;
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            
            bool isUser = IsUser();
            var themeList = new ThemeListViewModel() { };
            var vb = ViewBag;
            var themeListb = _themeService.GetAllThemes();
            //if (isUser)
            //{
            foreach (var theme in themeListb)
            {
                //_themeService.GetFilteredCoursesByTheme(theme);
                var count = _courseService.GetCoursesByTheme(theme).Count();
                themeList.Themes.Add(new ThemeView(theme.ThemeId,theme.Name,count));
            }

            //}

            return View(themeList);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            //ViewBag.Countries = _themeService.GetAllThemes();
            return View();
        }

        [HttpPost]
        public ActionResult Add(ThemeView theme)
        {

            var result = _themeService.AddTheme(new Theme(theme.ThemeEntityId,theme.Name));
            if (result)
            {
                return RedirectToAction("List");
            }
            //    flightResult = _flightService.CreateFlight(flight, request).Flight;
          //  }
          //  else
          //  {
             //   flightResult = _flightService.CreateFlight(flight);
          //  }
          //  Logger.Log.Info($"Flight with Id - {flightResult.Id}, created successfully.");
           // return RedirectToAction("Details", new { Id = flightResult.Id });
            //return Details(flightResult.Id);
            return RedirectToAction("Index","Home");
        }

        public ActionResult Delete(int themeId)
        {
            _themeService.DeleteTheme(themeId);
            return RedirectToAction("List");
        }
        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}