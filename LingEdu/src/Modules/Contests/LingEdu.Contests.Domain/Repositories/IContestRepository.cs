using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Contests.Domain.Contests;

namespace LingEdu.Contests.Domain.Repositories
{
    public interface IContestRepository
    {
        Task<List<Contest>> GetActiveAsync(DateTime utcNow, CancellationToken cancellationToken = default);

        Task<HashSet<Guid>> GetJoinedContestIdsAsync(Guid userId, IEnumerable<Guid> contestIds, CancellationToken cancellationToken = default);

        Task<Contest?> GetByIdAsync(Guid contestId, CancellationToken cancellationToken = default);

        Task<bool> IsJoinedAsync(Guid contestId, Guid userId, CancellationToken cancellationToken = default);

        Task JoinAsync(Guid contestId, Guid userId, DateTime joinedAt, CancellationToken cancellationToken = default);
    }
}
