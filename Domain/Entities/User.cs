using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public List<Order> Orders { get; set; } = new();
    }
}
