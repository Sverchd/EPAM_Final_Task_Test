using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using DataAccessLayer.Managers;
using Faculty.Filters;
using Faculty.Mappers;
using Faculty.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Faculty.Controllers
{
    [ExceptionFilter]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private DataAccessLayer.Interfaces.IUserService _userManager;

        public ProfileController()
        {
        }

        public ProfileController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        /// <summary>
        /// action for index page of profile
        /// </summary>
        /// <returns>view of profile</returns>
        [Authorize]
        public ActionResult Index()
        {
            var courses = new List<CourseView>();
            var profileModel = new ProfileViewModel();
            if (User.IsInRole("student"))
            {
                profileModel.role = "Student";
                courses = _courseService.GetCoursesByStudent(User.Identity.Name).Select(c => c.Map()).ToList();
                var grades = _courseService.GetGradebookForStudent(User.Identity.Name);
                profileModel.Grades = new List<GradeViewModel>();
                grades.ForEach(g=>profileModel.Grades.Add(new GradeViewModel(g.StudentUsername,g.CourseId,g.Grade)));
                profileModel.CoursesApplied = courses.Where(x => x.Start > DateTime.Now).ToList();
                profileModel.CoursesFinished = courses.Where(x => x.End < DateTime.Now).ToList();
                profileModel.CoursesInProgress =
                    courses.Where(x => x.Start < DateTime.Now && x.End > DateTime.Now).ToList();
            }
            else if (User.IsInRole("teacher"))
            {
                profileModel.role = "Teacher";
                courses = _courseService.GetCoursesByTeacher(User.Identity.Name).Select(c => c.Map()).ToList();
                profileModel.CoursesApplied = courses.Where(x => x.Start > DateTime.Now).ToList();
                profileModel.CoursesFinished = courses.Where(x => x.End < DateTime.Now).ToList();
                profileModel.CoursesInProgress =
                    courses.Where(x => x.Start < DateTime.Now && x.End > DateTime.Now).ToList();
            }
            else if (User.IsInRole("admin"))
            {
                profileModel.role = "Admin";
            }
            profileModel.Email = User.Identity.Name;
            return View(profileModel);
        }
    }
}