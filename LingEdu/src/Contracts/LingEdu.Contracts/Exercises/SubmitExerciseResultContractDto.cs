using System;

namespace LingEdu.Contracts.Exercises
{
    public sealed class SubmitExerciseResultContractDto
    {
        public Guid ExerciseId { get; init; }

        public int Points { get; init; }

        public bool IsCorrect { get; init; }

        public DateTime CompletedAt { get; init; }
    }
}
