using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Contracts.Subscriptions
{
    public sealed class SubscriptionPlanContractDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public string? Description { get; init; }

        public decimal Price { get; init; }

        public int DurationInDays { get; init; }
    }
}
