using System;
using LingEdu.Contests.Domain.Contests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Contests.Infrastructure.Persistence.Configurations
{
    internal sealed class ContestConfiguration : IEntityTypeConfiguration<Contest>
    {
        public void Configure(EntityTypeBuilder<Contest> builder)
        {
            builder.ToTable("Contests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.StartsAt).IsRequired();
            builder.Property(x => x.EndsAt).IsRequired();

            builder.HasData(new
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Weekly Challenge",
                StartsAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                EndsAt = new DateTime(2099, 12, 31, 0, 0, 0, DateTimeKind.Utc)
            });
        }
    }
}
