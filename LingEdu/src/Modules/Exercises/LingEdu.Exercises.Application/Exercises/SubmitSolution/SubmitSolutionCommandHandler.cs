using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Exercises;
using LingEdu.Exercises.Application.Common;
using LingEdu.Exercises.Domain.Progress;
using LingEdu.Exercises.Domain.Repositories;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Exercises.Application.Exercises.SubmitSolution
{
    internal sealed class SubmitSolutionCommandHandler
        : ICommandHandler<SubmitSolutionCommand, SubmitExerciseResultContractDto>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public SubmitSolutionCommandHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<SubmitExerciseResultContractDto>> Handle(SubmitSolutionCommand request, CancellationToken cancellationToken)
        {
            var exercise = await _exerciseRepository.GetExerciseWithQuestionsAsync(request.ExerciseId, cancellationToken);
            if (exercise is null)
            {
                return Result<SubmitExerciseResultContractDto>.Failure(ExerciseErrors.ExerciseNotFound);
            }

            if (exercise.IsPremium)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user is null)
                {
                    return Result<SubmitExerciseResultContractDto>.Failure(ExerciseErrors.UserNotFound);
                }

                if (!user.IsPremium)
                {
                    return Result<SubmitExerciseResultContractDto>.Failure(ExerciseErrors.PremiumRequired);
                }
            }

            if (exercise.Questions.Count == 0)
            {
                return Result<SubmitExerciseResultContractDto>.Failure(ExerciseErrors.InvalidSubmission);
            }

            var correctCount = 0;

            foreach (var question in exercise.Questions)
            {
                var answer = request.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                if (answer == default)
                {
                    continue;
                }

                var option = question.Options.FirstOrDefault(o => o.Id == answer.SelectedOptionId);
                if (option is not null && option.IsCorrect)
                {
                    correctCount++;
                }
            }

            var total = exercise.Questions.Count;
            var isCorrect = correctCount == total;
            var points = correctCount;

            var completedAt = DateTime.UtcNow;

            var progress = new UserProgress(
                Guid.NewGuid(),
                request.UserId,
                request.ExerciseId,
                points,
                isCorrect,
                completedAt);

            await _exerciseRepository.AddUserProgressAsync(progress, cancellationToken);

            var result = new SubmitExerciseResultContractDto
            {
                ExerciseId = request.ExerciseId,
                Points = points,
                IsCorrect = isCorrect,
                CompletedAt = completedAt
            };

            return Result<SubmitExerciseResultContractDto>.Success(result);
        }
    }
}
