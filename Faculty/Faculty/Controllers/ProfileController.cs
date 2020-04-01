using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using DataAccessLayer.Managers;
using Faculty.Mappers;
using Faculty.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Faculty.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private DataAccessLayer.Interfaces.IUserService _userManager;

        public ProfileController()
        {
        }

        public DataAccessLayer.Interfaces.IUserService UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            private set => _userManager = value;
        }

        public ProfileController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        // GET: Profile
        public ActionResult Index()
        {
            var courses = new List<CourseView>();
            var profileModel = new ProfileViewModel();
            if (User.IsInRole("student"))
            {
                profileModel.role = "Student";
                courses = _courseService.GetCoursesByStudent(User.Identity.Name).Select(c => c.Map()).ToList();
                profileModel.CoursesApplied = courses.Where(x => x.start > DateTime.Now).ToList();
                profileModel.CoursesFinished = courses.Where(x => x.end < DateTime.Now).ToList();
                profileModel.CoursesInProgress =
                    courses.Where(x => x.start < DateTime.Now && x.end > DateTime.Now).ToList();
            }
            else if (User.IsInRole("teacher"))
            {
                profileModel.role = "Teacher";
                //courses = _userService.GetTeacherByEmail(User.Identity.Name).Courses.Select(c => c.Map()).ToList();
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