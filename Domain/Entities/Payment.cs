namespace TransportRental.Models
{
    //Платіж
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalSum { get; set; }

        public Order Order { get; set; }
    }
}
