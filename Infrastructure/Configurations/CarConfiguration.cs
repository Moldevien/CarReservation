using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            /*builder.HasIndex(p => p.Id)
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
                .IsRequired();*/

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Brand)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Model)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.Year)
                .IsRequired()
                .HasColumnType("smallint"); // Оптимізація для зберігання року

            builder.Property(p => p.PricePerDay)
                .IsRequired()
                .HasColumnType("decimal(10,2)"); // 10 знаків, 2 після коми
        }
    }
}
