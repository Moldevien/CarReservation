using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class EditProfileViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Phone]
        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }
    }
}
