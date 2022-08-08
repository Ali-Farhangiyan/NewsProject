using System.ComponentModel.DataAnnotations;

namespace Admin.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? ReturnUrl { get; set; } 
        public bool IsRememberMe { get; set; }
    }
}
