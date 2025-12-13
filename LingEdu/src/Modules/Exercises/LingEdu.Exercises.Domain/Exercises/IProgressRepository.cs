namespace LingEdu.Exercises.Domain.Exercises;

public interface IProgressRepository
{
    Task SaveAsync(UserProgress progress, CancellationToken cancellationToken = default);
}
