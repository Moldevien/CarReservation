using System.ComponentModel.DataAnnotations;

namespace Web.Models.Order
{
    public class BookingViewModel
    {
        public int CarId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
