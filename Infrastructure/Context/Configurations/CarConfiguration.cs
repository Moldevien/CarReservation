using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Brand)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Model)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Gas)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Consumption)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Passengers)
                .IsRequired();

            builder.Property(p => p.Volume)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Gear_box)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.PricePerDay)
                .IsRequired()
                .HasColumnType("decimal(10,2)"); // 10 знаків, 2 після коми
        }
    }
}
