using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    //Автомобіль
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public short Year { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public decimal PricePerDay { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
