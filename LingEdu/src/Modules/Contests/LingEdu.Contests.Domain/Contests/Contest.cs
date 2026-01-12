using System;
using LingEdu.BuildingBlocks.Domain.Models;

namespace LingEdu.Contests.Domain.Contests
{
    public sealed class Contest : AggregateRoot
    {
        private Contest() { }

        public Contest(Guid id, string name, DateTime startsAt, DateTime endsAt)
            : base(id)
        {
            Name = name;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        public string Name { get; private set; } = default!;

        public DateTime StartsAt { get; private set; }

        public DateTime EndsAt { get; private set; }

        public bool IsActive(DateTime utcNow) => StartsAt <= utcNow && utcNow <= EndsAt;
    }
}
