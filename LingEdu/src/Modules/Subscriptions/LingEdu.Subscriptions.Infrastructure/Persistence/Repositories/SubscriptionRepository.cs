using LingEdu.Subscriptions.Domain;
using LingEdu.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LingEdu.Subscriptions.Infrastructure.Persistence.Repositories
{
    internal sealed class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionsDbContext _context;

        public SubscriptionRepository(SubscriptionsDbContext context)
        {
            _context = context;
        }

        public Task<SubscriptionPlan?> GetPlanByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Plans.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<UserSubscription?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return _context.UserSubscriptions
                .Include(x => x.Plan)
                .Where(x => x.UserId == userId && x.ActiveFrom <= now && x.ActiveTo >= now)
                .OrderByDescending(x => x.ActiveTo)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(UserSubscription subscription, CancellationToken cancellationToken = default)
        {
            await _context.UserSubscriptions.AddAsync(subscription, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<SubscriptionPlan>> GetAllPlansAsync(CancellationToken cancellationToken = default)
        {
            return _context.Plans.ToListAsync(cancellationToken);
        }
    }
}