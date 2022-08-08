using Admin.Model.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel LogIn { get; set; }
        public void OnGet(string returnUrl = "/")
        {
            ViewData["returnUrl"] = returnUrl;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await signInManager.SignOutAsync();

            var user = await userManager.FindByEmailAsync(LogIn.UserName);
            if (user is null)
            {
                ViewData["userNotFound"] = "Username Or Password is Incorrect!";
                return Page();
            }

            var result = await signInManager.PasswordSignInAsync(user, LogIn.Password, LogIn.IsRememberMe, true);

            if (result.Succeeded)
            {
                return RedirectToPage("/index");
            }

            ViewData["userNotFound"] = "Username Or Password is Incorrect!";
            return Page();
        }
    }
}
