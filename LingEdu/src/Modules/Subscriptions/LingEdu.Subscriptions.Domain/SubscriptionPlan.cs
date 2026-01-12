using LingEdu.BuildingBlocks.Domain.Models;
using System;

namespace LingEdu.Subscriptions.Domain
{
    public sealed class SubscriptionPlan : Entity
    {
        private SubscriptionPlan() { }

        public SubscriptionPlan(Guid id, string name, decimal price, int durationInDays) : base(id)
        {
            Name = name;
            Price = price;
            DurationInDays = durationInDays;
            IsActive = true;
        }

        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }
        public int DurationInDays { get; private set; }
        public bool IsActive { get; private set; }
    }
}