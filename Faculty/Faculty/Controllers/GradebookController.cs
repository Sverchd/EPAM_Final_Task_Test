using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using Faculty.Filters;
using Faculty.Mappers;
using Faculty.Models;
using Faculty.Utils;

namespace Faculty.Controllers
{
    [ExceptionFilter]
    public class GradebookController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IThemeService _themeService;

        public GradebookController(ICourseService courseService, IUserService userService, IThemeService themeService)
        {
            _courseService = courseService;
            _userService = userService;
            _themeService = themeService;
        }
        /// <summary>
        /// Get List action for getting gradebook
        /// </summary>
        /// <param name="courseId">id of requested course</param>
        /// <returns>view of gradebook list</returns>
        [HttpGet]
        [Authorize]
        public ActionResult List(int courseId)
        {
            var gradebook = _courseService.GetGradebookForCourse(courseId);
            var a = gradebook;
            List<GradeViewModel> grades= new List<GradeViewModel>();
            gradebook.ForEach(x=>grades.Add(new GradeViewModel(x.StudentUsername,courseId,x.Grade)));
            IList <GradeViewModel > igrades = grades;
            ViewBag.CourseId = courseId;
            return View(igrades);
        }
        /// <summary>
        /// Post action for saving gradebook
        /// </summary>
        /// <param name="igrades">list of marks</param>
        /// <returns>redirection to list action</returns>
        [HttpPost]
        [Authorize(Roles = "admin, teacher")]
        public ActionResult Save(IList<GradeViewModel> igrades)
        {
            var course = igrades[0].CourseId;
            var marks = new List<Mark>();
            foreach (var gradeViewModel in igrades)
            {
                marks.Add(new Mark(course,gradeViewModel.Student,gradeViewModel.Mark));
            }
            TempData["Success"] = "Gradebook successfully edited!";
            Logger.Log.Info($"Teacher with Name - {User.Identity.Name}, edited gradebook for course with ID - {course}");
            _courseService.SaveGradebookForCourse(marks);
            return RedirectToAction("List",new { courseId = course });
        }
    }
}