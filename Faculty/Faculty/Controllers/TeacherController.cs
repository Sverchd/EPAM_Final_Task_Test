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

        /// <summary>
        /// Get List action for teachers
        /// </summary>
        /// <returns>list view of teachers</returns>
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var teacherList = new TeacherListViewModel();
            var teachersListb = _userService.GetAllTeachers();
            teacherList.Teachers = teachersListb.Select(x => x.Map()).ToList();
            return View(teacherList);
        }
        /// <summary>
        /// Get action for adding teacher
        /// </summary>
        /// <returns>view for adding teacher</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// Post action for adding teacher
        /// </summary>
        /// <param name="user">user view model</param>
        /// <returns>redirect to list action or add view</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
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
        /// <summary>
        /// Get action for deleting teacher
        /// </summary>
        /// <param name="userEmail">user email</param>
        /// <returns>redirect to list action</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(string userEmail)
        {
            _userService.DeleteUser(userEmail);
            return RedirectToAction("List");
        }
    }
}