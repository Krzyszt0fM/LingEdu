using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Contests.Domain.Contests;
using LingEdu.Contests.Domain.Repositories;
using LingEdu.Contests.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Contests.Infrastructure.Persistence.Repositories
{
    internal sealed class ContestRepository : IContestRepository
    {
        private readonly ContestsDbContext _dbContext;

        public ContestRepository(ContestsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Contest>> GetActiveAsync(DateTime utcNow, CancellationToken cancellationToken = default)
        {
            return _dbContext.Contests
                .Where(c => c.StartsAt <= utcNow && utcNow <= c.EndsAt)
                .OrderBy(c => c.EndsAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<HashSet<Guid>> GetJoinedContestIdsAsync(Guid userId, IEnumerable<Guid> contestIds, CancellationToken cancellationToken = default)
        {
            var ids = await _dbContext.ContestParticipants
                .Where(p => p.UserId == userId && contestIds.Contains(p.ContestId))
                .Select(p => p.ContestId)
                .ToListAsync(cancellationToken);

            return ids.ToHashSet();
        }

        public Task<Contest?> GetByIdAsync(Guid contestId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Contests.SingleOrDefaultAsync(c => c.Id == contestId, cancellationToken);
        }

        public Task<bool> IsJoinedAsync(Guid contestId, Guid userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.ContestParticipants.AnyAsync(p => p.ContestId == contestId && p.UserId == userId, cancellationToken);
        }

        public async Task JoinAsync(Guid contestId, Guid userId, DateTime joinedAt, CancellationToken cancellationToken = default)
        {
            var participant = new ContestParticipant(Guid.NewGuid(), contestId, userId, joinedAt);

            await _dbContext.ContestParticipants.AddAsync(participant, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
