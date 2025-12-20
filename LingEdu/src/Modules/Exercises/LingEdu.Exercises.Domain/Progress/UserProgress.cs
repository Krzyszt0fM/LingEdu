using System;
using LingEdu.BuildingBlocks.Domain.Models;

namespace LingEdu.Exercises.Domain.Progress
{
    public sealed class UserProgress : Entity
    {
        private UserProgress()
        {
        }

        public UserProgress(Guid id, Guid userId, Guid exerciseId, int points, bool isCorrect, DateTime completedAt)
            : base(id)
        {
            UserId = userId;
            ExerciseId = exerciseId;
            Points = points;
            IsCorrect = isCorrect;
            CompletedAt = completedAt;
        }

        public Guid UserId { get; private set; }

        public Guid ExerciseId { get; private set; }

        public int Points { get; private set; }

        public bool IsCorrect { get; private set; }

        public DateTime CompletedAt { get; private set; }
    }
}
