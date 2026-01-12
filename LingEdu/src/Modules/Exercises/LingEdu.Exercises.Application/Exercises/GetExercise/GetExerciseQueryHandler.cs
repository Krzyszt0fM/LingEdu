using System.Threading;
using System.Threading.Tasks;
using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Exercises;
using LingEdu.Exercises.Application.Common;
using LingEdu.Exercises.Domain.Repositories;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Exercises.Application.Exercises.GetExercise
{
    internal sealed class GetExerciseQueryHandler
        : IQueryHandler<GetExerciseQuery, ExerciseContractDto>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public GetExerciseQueryHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<ExerciseContractDto>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            var exercise = await _exerciseRepository.GetExerciseWithQuestionsAsync(request.ExerciseId, cancellationToken);
            if (exercise is null)
            {
                return Result<ExerciseContractDto>.Failure(ExerciseErrors.ExerciseNotFound);
            }

            if (exercise.IsPremium)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user is null)
                {
                    return Result<ExerciseContractDto>.Failure(ExerciseErrors.UserNotFound);
                }

                if (!user.IsPremium)
                {
                    return Result<ExerciseContractDto>.Failure(ExerciseErrors.PremiumRequired);
                }
            }

            return Result<ExerciseContractDto>.Success(exercise.ToContractWithQuestions());
        }
    }
}
