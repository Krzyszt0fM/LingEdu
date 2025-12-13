using LingEdu.Subscriptions.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Subscriptions.Infrastructure.Persistence.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly SubscriptionsDbContext _dbContext;

    public SubscriptionRepository(SubscriptionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<SubscriptionPlan>> GetPlansAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Plans.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<UserSubscription?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _dbContext.UserSubscriptions.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task SaveUserSubscriptionAsync(UserSubscription subscription, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.UserSubscriptions.FirstOrDefaultAsync(x => x.UserId == subscription.UserId, cancellationToken);
        if (existing is null)
        {
            _dbContext.UserSubscriptions.Add(subscription);
        }
        else
        {
            existing = subscription;
            _dbContext.UserSubscriptions.Update(existing);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
