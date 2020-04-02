using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using Faculty.Mappers;
using Faculty.Models;

namespace Faculty.Controllers
{
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
                //_themeService.GetFilteredCoursesByTheme(theme);
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
            if (result) return RedirectToAction("List");

            ModelState.AddModelError("Name", "Theme already exists!");
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
            //var result = _themeService.AddTheme(new Theme(theme.ThemeEntityId, theme.Name));
            if (result) return RedirectToAction("List");

            ModelState.AddModelError("Name", "Theme already exists!");
            return View();
        }

        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}