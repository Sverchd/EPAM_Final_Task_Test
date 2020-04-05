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


        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var isUser = IsUser();
            var teacherList = new TeacherListViewModel();
            var vb = ViewBag;
            var teachersListb = _userService.GetAllTeachers();
            teacherList.Teachers = teachersListb.Select(x => x.Map()).ToList();
            return View(teacherList);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddUserViewModel user)
        {
            var result = _userService.AddTeacher(new UserView(user.Email, "teacher").MapFlat(), user.Password);
            if (result != null)
            {
                TempData["Success"] = "Teacher successfully created!";
                Logger.Log.Info($"Teacher with Name - {user.Email}, created successfully.");
                return RedirectToAction("List");
            }
            TempData["Error"] = "There is a user with such name!";

            return View();
        }

        [HttpGet]
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