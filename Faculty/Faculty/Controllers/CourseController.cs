using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using Faculty.Mappers;
using Faculty.Models;

namespace Faculty.Controllers
{
    [Authorize(Roles = "admin, student, teacher")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IThemeService _themeService;

        public CourseController(ICourseService courseService, IUserService userService, IThemeService themeService)
        {
            _courseService = courseService;
            _userService = userService;
            _themeService = themeService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult ListByTheme(int themeId)
        {
            var courseList = new CourseListViewModel();
            var theme = _themeService.GetThemeById(themeId);
            var courses = _courseService.GetCoursesByTheme(theme);
            courseList.Courses = courses.Select(x => x.Map()).ToList();
            ViewBag.Title = "Courses of " + theme.Name;
            return View("List", courseList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ListByTeacher(string userEmail)
        {
            var courseList = new CourseListViewModel();
            var courses = _courseService.GetCoursesByTeacher(userEmail);
            courseList.Courses = courses.Select(x => x.Map()).ToList();
            if (User.IsInRole("teacher"))
            {
                ViewBag.Title ="Your courses";
            }
            else
            {
                ViewBag.Title = userEmail + "`s courses";
            }
            
            return View("List", courseList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult List(string email)
        {
            var isUser = IsUser();
            var courseList = new CourseListViewModel();
            var vb = ViewBag;
            var courseListb = _courseService.GetAllCourses();
            //if (isUser)
            //{
            //courseList.user = _userService.get
            var clist = courseListb.Select(x => x.Map()).ToList();
            courseList.Courses = clist;
            //TODO: use mapper
            // foreach (var course in courseListb)
            //  {
            //
            //        courseList.Courses.Add(new CourseView(course.CourseId,new ThemeView(course.Theme.Name), course.name,course.start,course.end));
            //     }

            //}
            ViewBag.Title = "List";
            if (email != null)
                courseList.user = _userService.GetStudentByEmail(email).Map();
            return View(courseList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            var addCourseViewModel = new AddCourseViewModel();
            var teacherListView = new List<SelectListItem>();

            var teachers = _userService.GetAllTeachers();
            foreach (var teacher in teachers)
                teacherListView.Add(new SelectListItem {Value = teacher.Email, Text = teacher.Name});
            addCourseViewModel.Teachers = new SelectList(teacherListView, "Value", "Text");


            var themes = _themeService.GetAllThemes();
            var themeListView = new List<SelectListItem>();
            foreach (var theme in themes)
                themeListView.Add(new SelectListItem {Value = theme.ThemeId.ToString(), Text = theme.Name});
            addCourseViewModel.Teachers = new SelectList(teacherListView, "Value", "Text");
            addCourseViewModel.Themes = new SelectList(themeListView, "Value", "Text");
            addCourseViewModel.start = DateTime.Today;
            addCourseViewModel.end = DateTime.Today;
            return View(addCourseViewModel);
        }

        [HttpPost]
        public ActionResult Add(AddCourseViewModel addCourseViewModel)
        {
            var model = addCourseViewModel;
            var viewCourse = new Course();
            viewCourse.theme = _themeService.GetThemeById(addCourseViewModel.theme);
            viewCourse.name = addCourseViewModel.name;
            viewCourse.teacher = _userService.GetTeacherByEmail(addCourseViewModel.Teacher);
            viewCourse.start = addCourseViewModel.start;
            viewCourse.end = addCourseViewModel.end;
            _courseService.AddCourse(viewCourse);
            //use Get Theme by id
            ModelState.AddModelError("Name", "Course already exists!");
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int CourseId)
        {
            var addCourseViewModel = new AddCourseViewModel();
            var teacherListView = new List<SelectListItem>();

            var teachers = _userService.GetAllTeachers();
            foreach (var teacher in teachers)
                teacherListView.Add(new SelectListItem {Value = teacher.Email, Text = teacher.Name});
            addCourseViewModel.Teachers = new SelectList(teacherListView, "Value", "Text");


            var themes = _themeService.GetAllThemes();
            var themeListView = new List<SelectListItem>();
            foreach (var theme in themes)
                themeListView.Add(new SelectListItem {Value = theme.ThemeId.ToString(), Text = theme.Name});

            addCourseViewModel.CourseId = CourseId;
            addCourseViewModel.Teachers = new SelectList(teacherListView, "Value", "Text");
            addCourseViewModel.Themes = new SelectList(themeListView, "Value", "Text");
            var course = _courseService.GetCourseById(CourseId);
            addCourseViewModel.name = course.name;

            addCourseViewModel.Teacher = course.teacher.Name;
            addCourseViewModel.theme = course.theme.ThemeId;
            addCourseViewModel.start = course.start;
            addCourseViewModel.end = course.end;


            return View(addCourseViewModel);
        }

        [HttpPost]
        public ActionResult Edit(AddCourseViewModel addCourseViewModel)
        {
            var model = addCourseViewModel;
            var viewCourse = new Course();
            viewCourse.CourseId = addCourseViewModel.CourseId;
            viewCourse.theme = _themeService.GetThemeById(addCourseViewModel.theme);
            viewCourse.name = addCourseViewModel.name;
            viewCourse.teacher = _userService.GetTeacherByEmail(addCourseViewModel.Teacher);
            viewCourse.start = addCourseViewModel.start;
            viewCourse.end = addCourseViewModel.end;
            _courseService.EditCourse(viewCourse);
            //use Get Theme by id
            ModelState.AddModelError("Name", "Course already exists!");
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int CourseId)
        {
            _courseService.DeleteCourse(CourseId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Register(int CourseId)
        {
            var result = _courseService.Register(CourseId, User.Identity.Name);
            return RedirectToAction("List");
        }

        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}