using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    //Статус оренди
    public class OrderStatus
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути пустим")]
        public string Name { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new();
    }
}
