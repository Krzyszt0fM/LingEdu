using System;
using System.Threading;
using System.Threading.Tasks;

namespace LingEdu.Subscriptions.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<SubscriptionPlan?> GetPlanByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UserSubscription?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(UserSubscription subscription, CancellationToken cancellationToken = default);
        Task<List<SubscriptionPlan>> GetAllPlansAsync(CancellationToken cancellationToken = default);
    }
}