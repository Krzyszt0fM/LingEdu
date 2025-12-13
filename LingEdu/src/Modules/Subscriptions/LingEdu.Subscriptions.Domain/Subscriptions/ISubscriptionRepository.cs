namespace LingEdu.Subscriptions.Domain.Subscriptions;

public interface ISubscriptionRepository
{
    Task<List<SubscriptionPlan>> GetPlansAsync(CancellationToken cancellationToken = default);
    Task<UserSubscription?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SaveUserSubscriptionAsync(UserSubscription subscription, CancellationToken cancellationToken = default);
}
