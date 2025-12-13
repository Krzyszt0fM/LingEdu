using LingEdu.Exercises.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Exercises.Infrastructure.Persistence.Configurations;

public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.ToTable("QuizQuestions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Question).IsRequired();
        builder.HasMany(x => x.Options).WithOne(x => x.QuizQuestion).HasForeignKey(x => x.QuizQuestionId);
    }
}
