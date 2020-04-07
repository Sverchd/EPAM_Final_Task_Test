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
    public class StudentController : Controller
    {
        private readonly IUserService _userService;

        public StudentController()
        {
        }

        public StudentController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get action to get list of students
        /// </summary>
        /// <returns>View with list of students</returns>
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var studentList = new StudentListViewModel();
            var studentsListb = _userService.GetAllStudents();
            var bannedList = _userService.GetAllBanned();
            studentList.Students = studentsListb.Select(x => x.Map()).ToList();
            studentList.Banned = bannedList.Select(x => x.Map()).ToList();
            return View(studentList);
        }

        public ActionResult Ban(string userEmail)
        {
            var user = _userService.Ban(userEmail);
            if (user == null)
            {
                TempData["Error"] = "User wasn`t banned!";
            }
            TempData["Success"] = "Teacher successfully created!";
            Logger.Log.Info($"User with Name - {userEmail}, created successfully.");
            return RedirectToAction("List");
        }
        public ActionResult Activate(string userEmail)
        {
            var user = _userService.Activate(userEmail);
            if (user == null)
            {
                TempData["Error"] = "User wasn`t activated!";
            }
            TempData["Success"] = "Teacher successfully created!";
            Logger.Log.Info($"User with Name - {userEmail}, activated successfully.");
            return RedirectToAction("List");
        }
    }
}