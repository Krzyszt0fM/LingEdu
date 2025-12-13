using LingEdu.BuildingBlocks.Application;
using LingEdu.Contracts.Exercises;
using LingEdu.Exercises.Domain.Exercises;

namespace LingEdu.Exercises.Application.GetExercise;

public record GetExerciseQuery(Guid ExerciseId) : IQuery<ExerciseContractDto>;

public class GetExerciseQueryHandler : IQueryHandler<GetExerciseQuery, ExerciseContractDto>
{
    private readonly IExerciseRepository _repository;

    public GetExerciseQueryHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ExerciseContractDto>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var exercise = await _repository.GetByIdAsync(request.ExerciseId, cancellationToken);
        if (exercise is null)
        {
            return Result<ExerciseContractDto>.Failure(Error.NotFound("Exercise not found"));
        }

        var dto = new ExerciseContractDto(
            exercise.Id,
            exercise.Title,
            exercise.Level,
            exercise.IsPremium,
            exercise.Questions.Select(q => new QuizQuestionContractDto(q.Id, q.Question, q.Options.Select(o => new QuizOptionContractDto(o.Id, o.Text)).ToList())).ToList());

        return Result<ExerciseContractDto>.Success(dto);
    }
}
