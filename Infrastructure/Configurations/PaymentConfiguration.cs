using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportRental.Models;

namespace Infrastructure.Configurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            builder.HasIndex(p => p.Id)
                .IsUnique();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.TotalSum)
                .IsRequired();
        }
    }
}
