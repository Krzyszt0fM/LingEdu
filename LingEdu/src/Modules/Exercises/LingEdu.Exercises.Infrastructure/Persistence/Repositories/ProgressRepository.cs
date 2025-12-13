using LingEdu.Exercises.Domain.Exercises;

namespace LingEdu.Exercises.Infrastructure.Persistence.Repositories;

public class ProgressRepository : IProgressRepository
{
    private readonly ExercisesDbContext _dbContext;

    public ProgressRepository(ExercisesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(UserProgress progress, CancellationToken cancellationToken = default)
    {
        _dbContext.Progress.Add(progress);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
