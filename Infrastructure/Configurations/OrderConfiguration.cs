using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            /*builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder.HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            builder.HasOne(o => o.Car)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.CarId);

            builder.HasOne(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusId);

            builder.HasIndex(o => o.Id)
                .IsUnique();

            builder.Property(o => o.StartDate)
                .IsRequired();

            builder.Property(o => o.EndDate)
                .IsRequired();*/

            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder.HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Car)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusId);

            builder.Property(o => o.StartDate)
                .IsRequired()
                .HasDefaultValueSql("NOW()"); // За замовчуванням поточна дата

            builder.Property(o => o.EndDate)
                .IsRequired();
        }
    }
}
