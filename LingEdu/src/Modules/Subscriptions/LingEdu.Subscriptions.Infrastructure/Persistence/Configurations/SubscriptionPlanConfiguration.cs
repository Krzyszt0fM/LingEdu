using LingEdu.Subscriptions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LingEdu.Subscriptions.Infrastructure.Persistence.Configurations
{
    internal sealed class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.ToTable("SubscriptionPlans");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 2);

            builder.HasData(
                new SubscriptionPlan(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Free", 0m, 36500),
                new SubscriptionPlan(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Premium Monthly", 29.99m, 30)
            );
        }
    }
}