namespace TransportRental.Models
{
    //Автомобіль
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public short Year { get; set; }
        public decimal PricePerDay { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
