namespace TransportRental.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Access { get; set; } = false;

        public Client? Client { get; set; }
    }
}
