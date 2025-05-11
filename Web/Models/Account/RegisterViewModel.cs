using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ім’я користувача")]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [Display(Name = "Підтвердження паролю")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
