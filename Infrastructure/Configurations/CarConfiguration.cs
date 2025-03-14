using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportRental.Models;

namespace Infrastructure.Configurations
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasIndex(p => p.Id)
                .IsUnique();

            builder.Property(p => p.Brand)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Model)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Year)
                .IsRequired();

            builder.Property(p => p.PricePerDay)
                .IsRequired();
        }
    }
}
