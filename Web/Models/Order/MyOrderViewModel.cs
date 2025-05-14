namespace Web.Models.Order
{
    public class MyOrderViewModel
    {
        public string CarName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal PricePerDay { get; set; }

        public decimal TotalPrice => (EndDate - StartDate).Days * PricePerDay;
    }
}
