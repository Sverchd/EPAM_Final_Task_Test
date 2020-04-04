using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.Contracts;
using Faculty.Filters;
using Faculty.Mappers;
using Faculty.Models;

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


        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var isUser = IsUser();
            var studentList = new StudentListViewModel();
            var vb = ViewBag;
            var studentsListb = _userService.GetAllStudents();
            studentList.Students = studentsListb.Select(x => x.Map()).ToList();
            return View(studentList);
        }

        private bool IsUser()
        {
            return !(User.IsInRole("admin") || User.IsInRole("teacher"));
        }
    }
}