using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Contracts.Ranking;

namespace LingEdu.Ranking.Application.Services
{
    public interface IRankingService
    {
        Task AddPointsAsync(Guid userId, int points, string reason, CancellationToken cancellationToken = default);

        Task<List<RankingEntryContractDto>> GetTopAsync(int limit, CancellationToken cancellationToken = default);

        Task<UserScoreContractDto> GetUserScoreAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
