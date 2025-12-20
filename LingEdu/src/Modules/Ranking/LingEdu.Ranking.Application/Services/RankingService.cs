using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Contracts.Ranking;
using LingEdu.Ranking.Domain.Repositories;

namespace LingEdu.Ranking.Application.Services
{
    public sealed class RankingService : IRankingService
    {
        private readonly IRankingRepository _repository;

        public RankingService(IRankingRepository repository)
        {
            _repository = repository;
        }

        public Task AddPointsAsync(Guid userId, int points, string reason, CancellationToken cancellationToken = default)
        {
            return _repository.AddPointsAsync(userId, points, reason, cancellationToken);
        }

        public async Task<List<RankingEntryContractDto>> GetTopAsync(int limit, CancellationToken cancellationToken = default)
        {
            if (limit <= 0)
            {
                limit = 10;
            }

            var top = await _repository.GetTopNAsync(limit, cancellationToken);

            var result = top
                .Select((s, index) => new RankingEntryContractDto
                {
                    Rank = index + 1,
                    UserId = s.UserId,
                    TotalPoints = s.TotalPoints
                })
                .ToList();

            return result;
        }

        public async Task<UserScoreContractDto> GetUserScoreAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var score = await _repository.GetUserScoreAsync(userId, cancellationToken);

            return new UserScoreContractDto
            {
                UserId = userId,
                TotalPoints = score?.TotalPoints ?? 0
            };
        }
    }
}
