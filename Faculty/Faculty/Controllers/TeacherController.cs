using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using BusinessLogicLayer;
using BusinessLogicLayer.Contracts;
using BusinessLogicLayer.Models;
using Faculty.Mappers;
using Faculty.Models;
using Ninject.Infrastructure.Language;

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

            var result = _userService.AddTeacher(new UserView(user.Email,"teacher").MapFlat(), user.Password);
            if (result)
            {
                return RedirectToAction("List");
            }
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