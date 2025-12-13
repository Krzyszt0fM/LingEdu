using LingEdu.BuildingBlocks.Infrastructure;
using LingEdu.Subscriptions.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Subscriptions.Infrastructure.Persistence;

public class SubscriptionsDbContext : DbContextBase
{
    public DbSet<SubscriptionPlan> Plans => Set<SubscriptionPlan>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

    public SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.SubscriptionPlanConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.UserSubscriptionConfiguration());

        modelBuilder.Entity<SubscriptionPlan>().HasData(
            new SubscriptionPlan { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Free", Price = 0, Period = "Monthly" },
            new SubscriptionPlan { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Premium", Price = 9.99m, Period = "Monthly" }
        );

        base.OnModelCreating(modelBuilder);
    }
}
