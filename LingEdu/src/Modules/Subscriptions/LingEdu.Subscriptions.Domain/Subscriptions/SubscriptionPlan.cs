using LingEdu.BuildingBlocks.Domain;

namespace LingEdu.Subscriptions.Domain.Subscriptions;

public class SubscriptionPlan : Entity
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Period { get; private set; } = string.Empty;
}
