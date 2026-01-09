using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.Exercises.Domain.Exercises;
using LingEdu.Exercises.Domain.Progress;

namespace LingEdu.Exercises.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<Exercise?> GetExerciseWithQuestionsAsync(Guid exerciseId, CancellationToken cancellationToken = default);
        Task<List<Exercise>> GetForTodayAsync(CancellationToken cancellationToken = default);
        Task AddUserProgressAsync(UserProgress progress, CancellationToken cancellationToken = default);
        Task<List<UserProgress>> GetUserProgressAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
