using LingEdu.Ranking.Domain.Scores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Ranking.Infrastructure.Persistence.Configurations
{
    internal sealed class ScoreHistoryConfiguration : IEntityTypeConfiguration<ScoreHistory>
    {
        public void Configure(EntityTypeBuilder<ScoreHistory> builder)
        {
            builder.ToTable("ScoreHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();

            builder.Property(x => x.Points).IsRequired();

            builder.Property(x => x.Reason)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasIndex(x => new { x.UserId, x.CreatedAt });
        }
    }
}
