using System;
using LingEdu.BuildingBlocks.Domain.Models;

namespace LingEdu.Ranking.Domain.Scores
{
    public sealed class UserScore : AggregateRoot
    {
        private UserScore()
        {
        }

        public UserScore(Guid userId, int totalPoints)
            : base(userId)
        {
            TotalPoints = totalPoints;
        }

        public Guid UserId => Id;

        public int TotalPoints { get; private set; }

        public void AddPoints(int points)
        {
            if (points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }

            TotalPoints += points;
        }
    }
}
