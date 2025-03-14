using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportRental.Models;

namespace Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.UserId);

            builder.HasIndex(u => u.Id)
                .IsUnique();

            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
