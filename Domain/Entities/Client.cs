namespace TransportRental.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public User? User { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}