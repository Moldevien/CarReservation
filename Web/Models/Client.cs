using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required]
        [PhoneNumber]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}