using System;
using LingEdu.Exercises.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Exercises.Infrastructure.Persistence.Configurations
{
    internal sealed class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Prompt)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(x => x.Options)
                .WithOne()
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new QuizQuestion(
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    "Choose the correct form: She ___ to school every day."),
                new QuizQuestion(
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                    Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    "I ___ coffee in the morning."),

                new QuizQuestion(
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                    Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    "Choose the correct form: Yesterday, I ___ a movie."),
                new QuizQuestion(
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                    Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    "He ___ to work last Monday.")
            );
        }
    }
}
