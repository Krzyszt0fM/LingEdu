using LingEdu.Contests.Domain.Contests;
using LingEdu.Contests.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Contests.Infrastructure.Persistence
{
    public sealed class ContestsDbContext : DbContext
    {
        public ContestsDbContext(DbContextOptions<ContestsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contest> Contests => Set<Contest>();

        public DbSet<ContestParticipant> ContestParticipants => Set<ContestParticipant>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContestConfiguration());
            modelBuilder.ApplyConfiguration(new ContestParticipantConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
