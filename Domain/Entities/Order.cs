using Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    //Бронювання
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int CarId { get; set; }
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        [ValidDate(ErrorMessage = "Неправиль уведена дата")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        [ValidDate(ErrorMessage = "Неправиль уведена дата")]
        public DateTime EndDate { get; set; }

        public User User { get; set; } = null!;
        public Car Car { get; set; } = null!;
        public OrderStatus Status { get; set; } = null!;
        public List<Payment> Payments { get; set; } = new();
    }
}
