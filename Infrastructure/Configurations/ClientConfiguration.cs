using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportRental.Models;

namespace Infrastructure.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId);

            builder.HasIndex(c => c.Id)
                .IsUnique();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
