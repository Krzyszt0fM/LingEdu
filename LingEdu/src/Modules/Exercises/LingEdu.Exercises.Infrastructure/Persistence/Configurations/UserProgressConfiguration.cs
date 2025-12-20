using LingEdu.Exercises.Domain.Progress;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Exercises.Infrastructure.Persistence.Configurations
{
    internal sealed class UserProgressConfiguration : IEntityTypeConfiguration<UserProgress>
    {
        public void Configure(EntityTypeBuilder<UserProgress> builder)
        {
            builder.ToTable("UserProgress");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ExerciseId).IsRequired();
            builder.Property(x => x.Points).IsRequired();
            builder.Property(x => x.IsCorrect).IsRequired();
            builder.Property(x => x.CompletedAt).IsRequired();

            builder.HasIndex(x => new { x.UserId, x.CompletedAt });
        }
    }
}
