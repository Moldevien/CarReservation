using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder.HasOne(o => o.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.UserId)
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
