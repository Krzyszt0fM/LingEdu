using LingEdu.BuildingBlocks.Domain.Models;
using System;

namespace LingEdu.Subscriptions.Domain
{
    public sealed class UserSubscription : Entity
    {
        private UserSubscription() { }

        public UserSubscription(Guid id, Guid userId, Guid planId, DateTime activeFrom, DateTime activeTo) : base(id)
        {
            UserId = userId;
            SubscriptionPlanId = planId;
            ActiveFrom = activeFrom;
            ActiveTo = activeTo;
        }

        public Guid UserId { get; private set; }
        public Guid SubscriptionPlanId { get; private set; }
        public DateTime ActiveFrom { get; private set; }
        public DateTime ActiveTo { get; private set; }

        public SubscriptionPlan? Plan { get; private set; }

        public bool IsValid() => DateTime.UtcNow >= ActiveFrom && DateTime.UtcNow <= ActiveTo;
    }
}