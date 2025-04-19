using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasIndex(pt => pt.Id)
                .IsUnique();

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
