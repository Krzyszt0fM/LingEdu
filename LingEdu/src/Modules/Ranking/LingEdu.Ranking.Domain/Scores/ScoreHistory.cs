using System;
using LingEdu.BuildingBlocks.Domain.Models;

namespace LingEdu.Ranking.Domain.Scores
{
    public sealed class ScoreHistory : Entity
    {
        private ScoreHistory()
        {
        }

        public ScoreHistory(Guid id, Guid userId, int points, string reason, DateTime createdAt)
            : base(id)
        {
            UserId = userId;
            Points = points;
            Reason = reason;
            CreatedAt = createdAt;
        }

        public Guid UserId { get; private set; }

        public int Points { get; private set; }

        public string Reason { get; private set; } = default!;

        public DateTime CreatedAt { get; private set; }
    }
}
