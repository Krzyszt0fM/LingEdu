using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Ranking.Domain.Repositories;
using LingEdu.Ranking.Domain.Scores;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Ranking.Infrastructure.Persistence.Repositories
{
    internal sealed class RankingRepository : IRankingRepository
    {
        private readonly RankingDbContext _dbContext;

        public RankingRepository(RankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPointsAsync(Guid userId, int points, string reason, CancellationToken cancellationToken = default)
        {
            if (points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }

            var score = await _dbContext.UserScores
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (score is null)
            {
                score = new UserScore(userId, 0);
                await _dbContext.UserScores.AddAsync(score, cancellationToken);
            }

            score.AddPoints(points);

            var history = new ScoreHistory(
                Guid.NewGuid(),
                userId,
                points,
                reason,
                DateTime.UtcNow);

            await _dbContext.ScoreHistories.AddAsync(history, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserScore>> GetTopNAsync(int limit, CancellationToken cancellationToken = default)
        {
            if (limit <= 0)
            {
                limit = 10;
            }

            return await _dbContext.UserScores
                .OrderByDescending(x => x.TotalPoints)
                .ThenBy(x => x.Id)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

        public Task<UserScore?> GetUserScoreAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.UserScores
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        }
    }
}
