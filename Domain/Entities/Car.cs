namespace TransportRental.Models
{
    //Автомобіль
    public class Car
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
