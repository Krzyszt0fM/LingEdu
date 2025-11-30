using LingEdu.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Users.Infrastructure.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(u => u.Language)
                .IsRequired();

            builder.Property(u => u.IsPremium)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.UserName)
                .IsUnique();

            builder
                .HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    join => join.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    join => join.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    join =>
                    {
                        join.ToTable("UserRoles");
                        join.HasKey("UserId", "RoleId");
                    });
        }
    }
}
