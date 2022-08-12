using Admin.Model.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var user = new User
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.Email,

            };

            var result = await userManager.CreateAsync(user, register.Password);
            if (result.Succeeded) return RedirectToAction("Login");
            return View(register);
        }

        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            await signInManager.SignOutAsync();

            var user = await userManager.FindByEmailAsync(login.UserName);

            if(user is null)
            {
                ViewData["userNotFound"] = "Username Or Password is Incorrect!";
                return View(login);
            }
            var result = await signInManager.PasswordSignInAsync(user, login.Password, login.IsRememberMe, true);
            if (result.Succeeded) return RedirectToAction("Index","News");

            ViewData["userNotFound"] = "Username Or Password is Incorrect!";
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "News");
        }


    }
}
