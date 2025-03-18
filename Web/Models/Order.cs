using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    //Бронювання
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int StatusId { get; set; }
        [Required]
        [ValidDate]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        [ValidDate]
        public DateTime EndDate { get; set; }
    }
}
