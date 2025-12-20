using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Ranking.Domain.Scores;

namespace LingEdu.Ranking.Domain.Repositories
{
    public interface IRankingRepository
    {
        Task AddPointsAsync(Guid userId, int points, string reason, CancellationToken cancellationToken = default);

        Task<List<UserScore>> GetTopNAsync(int limit, CancellationToken cancellationToken = default);

        Task<UserScore?> GetUserScoreAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
