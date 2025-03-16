namespace TransportRental.Models
{
    //Бронювання
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public Client Client { get; set; } = null!;
        public Car Car { get; set; } = null!;
        public OrderStatus Status { get; set; } = null!;
        public List<Payment> Payments { get; set; } = new();
    }
}
