namespace TransportRental.Models
{
    //Бронювання
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Client Client { get; set; }
        public Car Car { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
