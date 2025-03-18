using Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [PhoneNumber]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public User? User { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}