using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TrainingNet.Models;
using TrainingNet.Models.Views;

namespace TrainingNet.Controllers
{
  [Route("[controller]"), Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public SignInManager<ApplicationUser> SignInManager
        {
            get { return _signInManager; }
        }
        
         public UserManager<ApplicationUser> UserManager
        {
            get { return _userManager; }
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet("Login"), AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost("Login"), AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid)
                return View(loginViewModel);
            var result = await SignInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
            if(!result.Succeeded)
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Register"), AllowAnonymous]
        public ActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if(!ModelState.IsValid)
                return View(registerViewModel);
            var user = new ApplicationUser 
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };
            var result = await UserManager.CreateAsync(user,registerViewModel.Password);
            if(!result.Succeeded)
                ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            await SignInManager.SignInAsync (user, isPersistent: true);
            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
