using System;
using LingEdu.BuildingBlocks.Domain.Models;

namespace LingEdu.Contests.Domain.Contests
{
    public sealed class ContestParticipant : Entity
    {
        private ContestParticipant() { }

        public ContestParticipant(Guid id, Guid contestId, Guid userId, DateTime joinedAt)
            : base(id)
        {
            ContestId = contestId;
            UserId = userId;
            JoinedAt = joinedAt;
        }

        public Guid ContestId { get; private set; }

        public Guid UserId { get; private set; }

        public DateTime JoinedAt { get; private set; }
    }
}
