using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using Faculty.Models;

namespace Faculty.Controllers
{
    [Authorize(Roles = "admin, student, teacher")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController()
        {

        }

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            bool isUser = IsUser();
            var courseList = new CourseListViewModel(){};
            var vb = ViewBag;
            var courseListb = _courseService.GetAllCourses();
            //if (isUser)
            //{
                foreach (var course in courseListb)
                {

                    courseList.Courses.Add(new CourseView(course.CourseId,new ThemeView(course.theme.Name), course.name,course.start,course.end));
                }

            //}
            
            return View(courseList);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}