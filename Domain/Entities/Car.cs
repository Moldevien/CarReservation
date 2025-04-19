using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
//using Domain.

namespace Domain.Entities
{
    //Автомобіль
    public class Car
    {
        public int Id { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        //[Display(Name = "", ResourceType = typeof())]
        //[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(CarModel))]
        public string Brand { get; set; } = null!;

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Model { get; set; } = null!;

        [Display(Name = "Type of gas")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Gas { get; set; } = null!;

        [Display(Name = "Gas consumption")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public double Consumption { get; set; }

        [Display(Name = "Passengers")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public int Passengers { get; set; }

        [Display(Name = "Engine volume")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public double Volume { get; set; }

        [Display(Name = "Gear box")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Gear_box { get; set; } = null!;

        [Display(Name = "Price per day")]
        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public decimal PricePerDay { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
