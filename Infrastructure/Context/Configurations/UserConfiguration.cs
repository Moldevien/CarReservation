using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            // Вказати, що в користувача може бути багато замовлень
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            /*builder.Property(u => u.Login)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false); // Логін - тільки латинські символи

            builder.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);*/
        }
    }
}
