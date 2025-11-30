using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Contracts.Subscriptions
{
    public sealed class SubscriptionStatusContractDto
    {
        public bool IsPremium { get; init; }

        public DateTime? ActiveTo { get; init; }

        public SubscriptionPlanContractDto? CurrentPlan { get; init; }
    }
}
