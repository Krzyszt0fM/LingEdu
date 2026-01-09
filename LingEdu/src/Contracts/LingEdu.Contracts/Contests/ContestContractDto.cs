using System;

namespace LingEdu.Contracts.Contests
{
    public sealed class ContestContractDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public DateTime StartsAt { get; init; }

        public DateTime EndsAt { get; init; }

        public bool IsJoined { get; init; }
    }
}
