using LingEdu.Ranking.Domain.Scores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Ranking.Infrastructure.Persistence.Configurations
{
    internal sealed class UserScoreConfiguration : IEntityTypeConfiguration<UserScore>
    {
        public void Configure(EntityTypeBuilder<UserScore> builder)
        {
            builder.ToTable("UserScores");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("UserId");

            builder.Property(x => x.TotalPoints)
                .IsRequired();

            builder.HasIndex(x => x.TotalPoints);
        }
    }
}
