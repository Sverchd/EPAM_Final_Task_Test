using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using Faculty.Filters;
using Faculty.Mappers;
using Faculty.Models;
using Faculty.Utils;

namespace Faculty.Controllers
{
    [Authorize(Roles = "admin, student, teacher")]
    [ExceptionFilter]
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
            var courses = _courseService.GetCoursesByTheme(themeId);
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
            var clist = courseListb.Select(x => x.Map()).ToList();
            courseList.Courses = clist;

            ViewBag.Title = "List";
            if (email != null)
                courseList.User = _userService.GetStudentByEmail(email).Map();
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
            addCourseViewModel.Start = DateTime.Today;
            addCourseViewModel.end = DateTime.Today;
            
            return View(addCourseViewModel);
        }

        [HttpPost]
        public ActionResult Add(AddCourseViewModel addCourseViewModel)
        {
            var model = addCourseViewModel;
            var viewCourse = new Course();
            if (_courseService.GetCourseByName(addCourseViewModel.Name) == null)
            {
                viewCourse.theme = _themeService.GetThemeById(addCourseViewModel.Theme);
                viewCourse.name = addCourseViewModel.Name;
                viewCourse.teacher = _userService.GetTeacherByEmail(addCourseViewModel.Teacher);
                viewCourse.start = addCourseViewModel.Start;
                viewCourse.end = addCourseViewModel.end;
                if (addCourseViewModel.Start<DateTime.Now||addCourseViewModel.Start>=addCourseViewModel.end)
                {
                    TempData["Error"] = "Wrong dates!";
                }
                else
                {
                    var newCourse = _courseService.AddCourse(viewCourse);
                    if (ModelState.IsValid && newCourse != null)
                    {
                        TempData["Success"] = "Course successfully created!";
                        Logger.Log.Info($"Course with Name - {viewCourse.name}, created successfully.");
                        return RedirectToAction("List");
                    }
                }
                
            }
            else
                TempData["Error"] = $"Course with Name {addCourseViewModel.Name} already exists!";
            Logger.Log.Info($"Course with Name - {addCourseViewModel.Name} wasn`t created, Course already exists!");
            return RedirectToAction("Add");
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
            addCourseViewModel.Name = course.name;

            addCourseViewModel.Teacher = course.teacher.Name;
            addCourseViewModel.Theme = course.theme.ThemeId;
            addCourseViewModel.Start = course.start;
            addCourseViewModel.end = course.end;


            return View(addCourseViewModel);
        }

        [HttpPost]
        public ActionResult Edit(AddCourseViewModel addCourseViewModel)
        {
            var model = addCourseViewModel;
            var viewCourse = new Course();
            viewCourse.CourseId = addCourseViewModel.CourseId;
            viewCourse.theme = _themeService.GetThemeById(addCourseViewModel.Theme);
            viewCourse.name = addCourseViewModel.Name;
            viewCourse.teacher = _userService.GetTeacherByEmail(addCourseViewModel.Teacher);
            viewCourse.start = addCourseViewModel.Start;
            viewCourse.end = addCourseViewModel.end;
            if (addCourseViewModel.Start < DateTime.Now || addCourseViewModel.Start >= addCourseViewModel.end)
            {
                TempData["Error"] = "Wrong dates!";
            }
            else
            {
                var course = _courseService.EditCourse(viewCourse);
                if (course != null)
                {
                    TempData["Success"] = "Course successfully edited!";
                    Logger.Log.Info($"Course with Name - {viewCourse.name}, edited successfully.");
                    return RedirectToAction("List");
                } 
                TempData["Error"] = "There is a course with such name!";
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int courseId)
        {
            if (_courseService.DeleteCourse(courseId))
            {
                TempData["Success"] = "Course was successfully deleted!";
                Logger.Log.Info($"Course with ID - {courseId}, deleted successfully.");
            }
            else
                TempData["Error"] = "No such course!";
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Register(int CourseId)
        {
            var result = _courseService.Register(CourseId, User.Identity.Name);
            TempData["Success"] = "User was successfully registered for course!";
            Logger.Log.Info($"User with name - {User.Identity.Name}");
            return RedirectToAction("List");
        }

        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}