using System;

namespace LingEdu.Contracts.Ranking
{
    public sealed class RankingEntryContractDto
    {
        public int Rank { get; init; }

        public Guid UserId { get; init; }

        public int TotalPoints { get; init; }
    }

    public sealed class UserScoreContractDto
    {
        public Guid UserId { get; init; }

        public int TotalPoints { get; init; }
    }
}
