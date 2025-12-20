using System;
using LingEdu.Exercises.Domain.Enums;
using LingEdu.Exercises.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Exercises.Infrastructure.Persistence.Configurations
{
    internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercises");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Level).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.IsPremium).IsRequired();

            builder.HasMany(x => x.Questions)
                .WithOne()
                .HasForeignKey(q => q.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Exercise(
                    Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    "Present Simple Basics",
                    ExerciseLevel.A1,
                    ExerciseType.Quiz,
                    false),
                new Exercise(
                    Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    "Past Simple Challenge",
                    ExerciseLevel.A2,
                    ExerciseType.Quiz,
                    true)
            );
        }
    }
}
