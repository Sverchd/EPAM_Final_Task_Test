using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using Faculty.Filters;
using Faculty.Mappers;
using Faculty.Models;

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
            //if (isUser)
            //{
            teacherList.Teachers = teachersListb.Select(x => x.Map()).ToList();

            //}
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
            if (result) return RedirectToAction("List");
            ModelState.AddModelError("Name", "User already exists!");
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