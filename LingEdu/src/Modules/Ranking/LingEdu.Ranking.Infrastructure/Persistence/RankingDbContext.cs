using LingEdu.Ranking.Domain.Scores;
using LingEdu.Ranking.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Ranking.Infrastructure.Persistence
{
    public sealed class RankingDbContext : DbContext
    {
        public RankingDbContext(DbContextOptions<RankingDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserScore> UserScores => Set<UserScore>();

        public DbSet<ScoreHistory> ScoreHistories => Set<ScoreHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserScoreConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreHistoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
