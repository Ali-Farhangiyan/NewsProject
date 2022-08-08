using Admin.Model.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public void OnGet()
        {

        }

        [BindProperty]
        public RegisterViewModel Register { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new User
            {
                Email = Register.Email,
                UserName = Register.Email,
                FullName = Register.FullName,

            };

            var result = await userManager.CreateAsync(user, Register.Password);

            if (result.Succeeded)
            {
                return RedirectToPage("LogOn");
            }

            return Page();
        }
    }
}
