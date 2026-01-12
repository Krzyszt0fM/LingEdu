using System;
using LingEdu.Exercises.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Exercises.Infrastructure.Persistence.Configurations
{
    internal sealed class QuizOptionConfiguration : IEntityTypeConfiguration<QuizOption>
    {
        public void Configure(EntityTypeBuilder<QuizOption> builder)
        {
            builder.ToTable("QuizOptions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.IsCorrect)
                .IsRequired();

            builder.HasData(
                new QuizOption(Guid.Parse("c1111111-1111-1111-1111-111111111111"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "goes", true),
                new QuizOption(Guid.Parse("c1111111-1111-1111-1111-111111111112"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "go", false),
                new QuizOption(Guid.Parse("c1111111-1111-1111-1111-111111111113"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "going", false),
                new QuizOption(Guid.Parse("c1111111-1111-1111-1111-111111111114"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "gone", false),

                new QuizOption(Guid.Parse("c2222222-2222-2222-2222-222222222221"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drink", true),
                new QuizOption(Guid.Parse("c2222222-2222-2222-2222-222222222222"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drinks", false),
                new QuizOption(Guid.Parse("c2222222-2222-2222-2222-222222222223"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drank", false),
                new QuizOption(Guid.Parse("c2222222-2222-2222-2222-222222222224"), Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "drinking", false),

                new QuizOption(Guid.Parse("d1111111-1111-1111-1111-111111111111"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watched", true),
                new QuizOption(Guid.Parse("d1111111-1111-1111-1111-111111111112"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watch", false),
                new QuizOption(Guid.Parse("d1111111-1111-1111-1111-111111111113"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watches", false),
                new QuizOption(Guid.Parse("d1111111-1111-1111-1111-111111111114"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"), "watching", false),

                new QuizOption(Guid.Parse("d2222222-2222-2222-2222-222222222221"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "went", true),
                new QuizOption(Guid.Parse("d2222222-2222-2222-2222-222222222222"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "go", false),
                new QuizOption(Guid.Parse("d2222222-2222-2222-2222-222222222223"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "goes", false),
                new QuizOption(Guid.Parse("d2222222-2222-2222-2222-222222222224"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), "going", false)
            );
        }
    }
}
