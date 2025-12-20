using System;
using System.Collections.Generic;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Exercises;

namespace LingEdu.Exercises.Application.Exercises.SubmitSolution
{
    public sealed record SubmitSolutionAnswer(Guid QuestionId, Guid SelectedOptionId);

    public sealed class SubmitSolutionCommand : ICommand<SubmitExerciseResultContractDto>
    {
        public SubmitSolutionCommand(Guid userId, Guid exerciseId, IReadOnlyCollection<SubmitSolutionAnswer> answers)
        {
            UserId = userId;
            ExerciseId = exerciseId;
            Answers = answers;
        }

        public Guid UserId { get; }

        public Guid ExerciseId { get; }

        public IReadOnlyCollection<SubmitSolutionAnswer> Answers { get; }
    }
}
