using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportRental.Models;

namespace Infrastructure.Configurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            /*builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            builder.HasIndex(p => p.Id)
                .IsUnique();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.TotalSum)
                .IsRequired();*/

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Date)
                .IsRequired()
                .HasDefaultValueSql("NOW()"); // Автоматична дата

            builder.Property(p => p.TotalSum)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        }
    }
}
