using LingEdu.Subscriptions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingEdu.Subscriptions.Infrastructure.Persistence.Configurations
{
    internal sealed class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.ToTable("UserSubscriptions");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.Plan)
                   .WithMany()
                   .HasForeignKey(x => x.SubscriptionPlanId);
        }
    }
}