using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    //Автомобіль
    public class Car
    {
        public int Id { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Brand { get; set; } = null!;

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Model { get; set; } = null!;

        [Display(Name = "Gas")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Gas { get; set; } = null!;

        [Display(Name = "Consumption")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public double Consumption { get; set; }

        [Display(Name = "Passengers")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public int Passengers { get; set; }

        [Display(Name = "Volume")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public double Volume { get; set; }

        [Display(Name = "Gear_box")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Gear_box { get; set; } = null!;

        [Display(Name = "PricePerDay")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public decimal PricePerDay { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
