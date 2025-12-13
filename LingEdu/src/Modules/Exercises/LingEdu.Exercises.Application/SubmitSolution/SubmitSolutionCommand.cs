using LingEdu.BuildingBlocks.Application;
using LingEdu.Contracts.Exercises;
using LingEdu.Exercises.Domain.Exercises;

namespace LingEdu.Exercises.Application.SubmitSolution;

public record SubmitSolutionCommand(Guid ExerciseId, Guid UserId, IReadOnlyList<Guid> SelectedOptions) : ICommand<ExerciseResultDto>;

public class SubmitSolutionCommandHandler : ICommandHandler<SubmitSolutionCommand, ExerciseResultDto>
{
    private readonly IExerciseRepository _repository;
    private readonly IProgressRepository _progressRepository;

    public SubmitSolutionCommandHandler(IExerciseRepository repository, IProgressRepository progressRepository)
    {
        _repository = repository;
        _progressRepository = progressRepository;
    }

    public async Task<Result<ExerciseResultDto>> Handle(SubmitSolutionCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _repository.GetByIdAsync(request.ExerciseId, cancellationToken);
        if (exercise is null)
        {
            return Result<ExerciseResultDto>.Failure(Error.NotFound("Exercise not found"));
        }

        var correctOptionIds = exercise.Questions.SelectMany(q => q.Options.Where(o => o.IsCorrect)).Select(o => o.Id).ToHashSet();
        var isCorrect = request.SelectedOptions.All(correctOptionIds.Contains);
        var score = isCorrect ? 10 : 0;

        await _progressRepository.SaveAsync(new UserProgress
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ExerciseId = request.ExerciseId,
            Completed = true,
            Score = score
        }, cancellationToken);

        return Result<ExerciseResultDto>.Success(new ExerciseResultDto(isCorrect, score));
    }
}
