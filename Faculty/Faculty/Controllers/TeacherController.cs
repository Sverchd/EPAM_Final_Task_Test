using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using Faculty.Models;

namespace Faculty.Controllers
{
    public class TeacherController : Controller
    {

        private readonly IUserService _userService;

        public TeacherController()
        {

        }

        public TeacherController(IUserService userService)
        {
            
            _userService = userService;
        }
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {

            bool isUser = IsUser();
            var teacherList = new TeacherListViewModel();
            var vb = ViewBag;
            var teachersListb = _userService.GetAllTeachers();
            //if (isUser)
            //{
            foreach (var teacher in teachersListb)
            {
                //_themeService.GetFilteredCoursesByTheme(theme);
                var count =teacher.Courses.Count();
                
                teacherList.Teachers.Add(new UserView(teacher.Name,teacher.Role,count));
            }

            //}

            return View(teacherList);
        }
        public ActionResult Delete(string userEmail)
        {
            _userService.DeleteTeacher(userEmail);
            return RedirectToAction("List");
        }
        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}