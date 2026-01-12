using System;
using System.Collections.Generic;

namespace LingEdu.Contracts.Reports
{
    public sealed class UserProgressEntryContractDto
    {
        public Guid ExerciseId { get; init; }

        public int Points { get; init; }

        public bool IsCorrect { get; init; }

        public DateTime CompletedAt { get; init; }
    }

    public sealed class UserReportContractDto
    {
        public Guid UserId { get; init; }

        public int TotalPoints { get; init; }

        public int CompletedExercisesCount { get; init; }

        public DateTime? LastCompletedAt { get; init; }

        public List<UserProgressEntryContractDto> RecentProgress { get; init; } = new();
    }
}
