using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Exercises.Domain.Exercises;
using LingEdu.Exercises.Domain.Progress;
using LingEdu.Exercises.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Exercises.Infrastructure.Persistence.Repositories
{
    internal sealed class ExerciseRepository : IExerciseRepository
    {
        private readonly ExercisesDbContext _dbContext;

        public ExerciseRepository(ExercisesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Exercise?> GetExerciseWithQuestionsAsync(Guid exerciseId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Exercises
                .Include(e => e.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(e => e.Id == exerciseId, cancellationToken);
        }

        public async Task<List<Exercise>> GetForTodayAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Exercises
                .OrderBy(e => e.Level)
                .ThenBy(e => e.Title)
                .Take(3)
                .ToListAsync(cancellationToken);
        }

        public async Task AddUserProgressAsync(UserProgress progress, CancellationToken cancellationToken = default)
        {
            await _dbContext.UserProgress.AddAsync(progress, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public Task<List<UserProgress>> GetUserProgressAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.UserProgress
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CompletedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
