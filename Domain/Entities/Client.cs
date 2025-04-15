using Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        //[MaxLength(60)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        [PhoneNumber(ErrorMessage = "Неправильно уведено номер телефону")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        [EmailAddress(ErrorMessage = "Неправильно уведено адресу пошти")]
        public string Email { get; set; } = null!;

        public User? User { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}