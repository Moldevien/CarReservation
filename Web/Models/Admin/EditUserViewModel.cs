using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Admin
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

        public List<string> AllRoles { get; set; } = new();
    }
}
