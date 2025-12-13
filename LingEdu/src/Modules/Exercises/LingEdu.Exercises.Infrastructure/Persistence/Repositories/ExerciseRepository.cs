using LingEdu.Exercises.Domain.Exercises;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Exercises.Infrastructure.Persistence.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly ExercisesDbContext _dbContext;

    public ExerciseRepository(ExercisesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Exercise exercise, CancellationToken cancellationToken = default)
    {
        _dbContext.Exercises.Add(exercise);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Exercises.Include(x => x.Questions).ThenInclude(q => q.Options).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<Exercise>> GetByLevelAsync(string level, CancellationToken cancellationToken = default)
    {
        return _dbContext.Exercises.Where(x => x.Level == level).ToListAsync(cancellationToken);
    }
}
