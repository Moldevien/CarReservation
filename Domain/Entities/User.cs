using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        [MinLength(6, ErrorMessage = "Логін повинен бути не менше 6 символів")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        [MinLength(10, ErrorMessage = "Пароль повинен бути не менше 10 символів")]
        public string Password { get; set; } = string.Empty;
        public bool Access { get; set; } = false;
        public Client Client { get; set; } = null!;
    }
}
