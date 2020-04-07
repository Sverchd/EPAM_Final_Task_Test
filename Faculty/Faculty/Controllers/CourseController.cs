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

        /// <summary>
        /// Get list action of course
        /// </summary>
        /// <param name="themeId">id of requested theme</param>
        /// <returns>list view of courses</returns>
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
        /// <summary>
        /// Get action for getting list of courses by teacher
        /// </summary>
        /// <param name="userEmail">username of teacher</param>
        /// <returns>list of courses for selected teacher</returns>
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
        /// <summary>
        /// Get action getting list of all teachers
        /// </summary>
        /// <param name="email"></param>
        /// <returns>view for list of teachers</returns>
        [HttpGet]
        [Authorize]
        public ActionResult List(string email)
        {
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
        /// <summary>
        /// Get Add action for adding course
        /// </summary>
        /// <returns>view for adding course</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
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
        /// <summary>
        /// Post action for adding course
        /// </summary>
        /// <param name="addCourseViewModel">add course view model</param>
        /// <returns>view for list of courses or add view depends on result</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Add(AddCourseViewModel addCourseViewModel)
        {
            var model = addCourseViewModel;
            var viewCourse = new Course();
            if (_courseService.GetCourseByName(addCourseViewModel.Name) == null)
            {
                viewCourse.Theme = _themeService.GetThemeById(addCourseViewModel.Theme);
                viewCourse.Name = addCourseViewModel.Name;
                viewCourse.Teacher = _userService.GetTeacherByEmail(addCourseViewModel.Teacher);
                viewCourse.Start = addCourseViewModel.Start;
                viewCourse.End = addCourseViewModel.end;
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
                        Logger.Log.Info($"Course with Name - {viewCourse.Name}, created successfully.");
                        return RedirectToAction("List");
                    }
                }
                
            }
            else
                TempData["Error"] = $"Course with Name {addCourseViewModel.Name} already exists!";
            Logger.Log.Info($"Course with Name - {addCourseViewModel.Name} wasn`t created, Course already exists!");
            return RedirectToAction("Add");
        }
        /// <summary>
        /// Get action for editing course
        /// </summary>
        /// <param name="CourseId">id of selected course</param>
        /// <returns>view with course for editing</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
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
            addCourseViewModel.Name = course.Name;

            addCourseViewModel.Teacher = course.Teacher.Name;
            addCourseViewModel.Theme = course.Theme.ThemeId;
            addCourseViewModel.Start = course.Start;
            addCourseViewModel.end = course.End;

            return View(addCourseViewModel);
        }
        /// <summary>
        /// Post method for editing course
        /// </summary>
        /// <param name="addCourseViewModel">Model of course to add</param>
        /// <returns>redirects to list action</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(AddCourseViewModel addCourseViewModel)
        {
            var model = addCourseViewModel;
            var viewCourse = new Course();
            viewCourse.CourseId = addCourseViewModel.CourseId;
            viewCourse.Theme = _themeService.GetThemeById(addCourseViewModel.Theme);
            viewCourse.Name = addCourseViewModel.Name;
            viewCourse.Teacher = _userService.GetTeacherByEmail(addCourseViewModel.Teacher);
            viewCourse.Start = addCourseViewModel.Start;
            viewCourse.End = addCourseViewModel.end;
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
                    Logger.Log.Info($"Course with Name - {viewCourse.Name}, edited successfully.");
                    return RedirectToAction("List");
                } 
                TempData["Error"] = "There is a course with such name!";
            }
            return RedirectToAction("List");
        }
        /// <summary>
        /// Get action for deleting course
        /// </summary>
        /// <param name="courseId">id of selected course</param>
        /// <returns>redirect to list action</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
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
        /// <summary>
        /// Get action for registering for course
        /// </summary>
        /// <param name="CourseId">id of selected course</param>
        /// <returns>redirect to lis action</returns>
        [HttpGet]
        [Authorize(Roles = "student")]
        public ActionResult Register(int CourseId)
        {
            var result = _courseService.Register(CourseId, User.Identity.Name);
            TempData["Success"] = "User was successfully registered for course!";
            Logger.Log.Info($"User with name - {User.Identity.Name}");
            return RedirectToAction("List");
        }
    }
}