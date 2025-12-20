using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Exercises;
using LingEdu.Exercises.Domain.Repositories;

namespace LingEdu.Exercises.Application.Exercises.GetForToday
{
    internal sealed class GetExercisesForTodayQueryHandler
        : IQueryHandler<GetExercisesForTodayQuery, List<ExerciseContractDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public GetExercisesForTodayQueryHandler(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Result<List<ExerciseContractDto>>> Handle(
            GetExercisesForTodayQuery request,
            CancellationToken cancellationToken)
        {
            var exercises = await _exerciseRepository.GetForTodayAsync(cancellationToken);

            var dto = exercises.Select(e => e.ToContract()).ToList();

            return Result<List<ExerciseContractDto>>.Success(dto);
        }
    }
}
