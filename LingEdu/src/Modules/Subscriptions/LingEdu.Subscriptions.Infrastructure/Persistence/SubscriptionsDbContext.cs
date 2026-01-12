using LingEdu.Subscriptions.Domain;
using LingEdu.Subscriptions.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Subscriptions.Infrastructure.Persistence
{
    public sealed class SubscriptionsDbContext : DbContext
    {
        public SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> options) : base(options)
        {
        }

        public DbSet<SubscriptionPlan> Plans => Set<SubscriptionPlan>();
        public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new UserSubscriptionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}