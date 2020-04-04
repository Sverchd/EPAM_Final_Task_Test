using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using Faculty.Filters;
using Faculty.Mappers;
using Faculty.Models;
using Faculty.Utils;

namespace Faculty.Controllers
{
    [ExceptionFilter]
    public class ThemeController : Controller
    {
        // GET: Theme
        private readonly IThemeService _themeService;
        private readonly ICourseService _courseService;

        public ThemeController(IThemeService themeService, ICourseService courseService)
        {
            _themeService = themeService;
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var isUser = IsUser();
            var themeList = new ThemeListViewModel();
            var vb = ViewBag;
            var themeListb = _themeService.GetAllThemes();
            //if (isUser)
            //{
            foreach (var theme in themeListb)
            {
                //_themeService.GetFilteredCoursesByTheme(Theme);
                var count = _courseService.GetCoursesByTheme(theme).Count();
                themeList.Themes.Add(theme.Map(count));
            }

            //}

            return View(themeList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ThemeView theme)
        {
            var result = _themeService.AddTheme(theme.Map());
            if (result != null)
            {
                TempData["Success"] = "Theme successfully created!";
                Logger.Log.Info($"Theme with Name - {theme.Name}, created successfully.");
                return RedirectToAction("List");
            }
            Logger.Log.Info($"Theme with Name - {theme.Name}, already exists!");
            TempData["Error"] = "Theme already exists!";
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int themeId)
        {
            _themeService.DeleteTheme(themeId);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int themeId)
        {
            var theme = _themeService.GetThemeById(themeId);
            return View(theme.Map());
        }

        [HttpPost]
        public ActionResult Edit(ThemeView theme)
        {
            var result = _themeService.Edit(theme.Map());
            //var result = _themeService.AddTheme(new Theme(Theme.ThemeEntityId, Theme.Name));
            if (result != null)
            {
                TempData["Success"] = "Theme successfully modified!";
                Logger.Log.Info($"Theme with Name - {theme.Name}, modified.");
                return RedirectToAction("List");
            }

            ModelState.AddModelError("Name", "Theme already exists!");
            Logger.Log.Info($"Theme with Name - {theme.Name}, wasn`t modified!");
            TempData["Error"] = "Theme wasn`t modified!";
            return View();
        }

        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}