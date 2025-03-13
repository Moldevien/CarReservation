using Microsoft.EntityFrameworkCore;
using TransportRental.Models;

namespace TransportRental.Infrastructure.Data
{
    public  class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> RentalStatuses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.UserId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Car)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.CarId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Car>()
                .HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<Car>()
                .Property(p => p.Brand).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Car>()
                .Property(p => p.Model).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Car>()
                .Property(p => p.Year).IsRequired();
            modelBuilder.Entity<Car>()
                .Property(p => p.PricePerDay).IsRequired();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Client>()
                .Property(c => c.Name).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Client>()
                .Property(c => c.Phone).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Client>()
                .Property(c => c.Email).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<Order>()
                .Property(o => o.StartDate).IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.EndDate).IsRequired();

            modelBuilder.Entity<OrderStatus>()
                .HasIndex(pt => pt.Id).IsUnique();
            modelBuilder.Entity<OrderStatus>()
                .Property(pt => pt.Name).IsRequired().HasMaxLength(30);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Id).IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Login).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>()
                .Property(u => u.Password).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<Payment>()
                .Property(p => p.Date).IsRequired();
            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalSum).IsRequired();
        }
    }
}
