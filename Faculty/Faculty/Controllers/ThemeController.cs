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
        private readonly IThemeService _themeService;
        private readonly ICourseService _courseService;

        public ThemeController(IThemeService themeService, ICourseService courseService)
        {
            _themeService = themeService;
            _courseService = courseService;
        }
        /// <summary>
        /// Get action for list
        /// </summary>
        /// <returns>list view</returns>
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var themeList = new ThemeListViewModel();
            themeList.Themes = _themeService.GetAllThemes().Select(x=>x.Map()).ToList();
            //foreach (var theme in themeListb)
            //{
            //    var count = _courseService.GetCoursesByTheme(theme.ThemeId).Count();
            //    themeList.Themes.Add(theme.Map(count));
            //}
            
            return View(themeList);
        }
        /// <summary>
        /// Get action for adding theme
        /// </summary>
        /// <returns>add view</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// Post action for adding theme 
        /// </summary>
        /// <param name="theme">Theme view model</param>
        /// <returns>redirect to list action or add view</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
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
        /// <summary>
        /// Get action for deleting theme
        /// </summary>
        /// <param name="themeId">id of selected theme</param>
        /// <returns>redirect to list action</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int themeId)
        {
            _themeService.DeleteTheme(themeId);
            return RedirectToAction("List");
        }
        /// <summary>
        /// Get action for editing theme
        /// </summary>
        /// <param name="themeId">id of selected theme</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int themeId)
        {
            var theme = _themeService.GetThemeById(themeId);
            return View(theme.Map());
        }
        /// <summary>
        /// Post action for editing theme
        /// </summary>
        /// <param name="theme">theme view model</param>
        /// <returns>redirect to list action or edit view</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(ThemeView theme)
        {
            var result = _themeService.Edit(theme.Map());
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
    }
}