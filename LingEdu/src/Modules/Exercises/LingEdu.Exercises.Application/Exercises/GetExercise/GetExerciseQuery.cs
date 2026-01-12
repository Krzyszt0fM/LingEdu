using System;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Exercises;

namespace LingEdu.Exercises.Application.Exercises.GetExercise
{
    public sealed class GetExerciseQuery : IQuery<ExerciseContractDto>
    {
        public GetExerciseQuery(Guid userId, Guid exerciseId)
        {
            UserId = userId;
            ExerciseId = exerciseId;
        }

        public Guid UserId { get; }

        public Guid ExerciseId { get; }
    }
}
