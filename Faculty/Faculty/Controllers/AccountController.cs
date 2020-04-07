using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Managers;
using DataAccessLayer.Models;
using Faculty.Filters;
using Faculty.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Faculty.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private IUserService _userManager;
        private BusinessLogicLayer.Contracts.IUserService _userService;

        /// <summary>
        /// default constructor
        /// </summary>
        public AccountController()
        {
        }
        /// <summary>
        /// constructor with parameters
        /// </summary>
        /// <param name="userService">user service interface</param>
        public AccountController(BusinessLogicLayer.Contracts.IUserService userService)
        {
            _userService = userService;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public IUserService UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            private set => _userManager = value;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;


        /// <summary>
        /// Login action
        /// </summary>
        /// <param name="returnUrl">url</param>
        /// <returns>Login view</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Login action
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            var user = SignInManager.UserManager.Users.SingleOrDefault(x => x.Email == model.Email);
            if (user!=null&&UserManager.IsInRole(user.Id, "banned"))
            {
                return View("Banned");
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            
            switch (result)
            {
                case SignInStatus.Success:

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        /// <summary>
        /// Get action for registration 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// register action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser {UserName = model.Email, Email = model.Email};

                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "student");
                    await SignInManager.SignInAsync(user, false, false);
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            return View(model);
        }


        /// <summary>
        /// logoff action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error);
        }
    }
}