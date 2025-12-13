using LingEdu.BuildingBlocks.Domain;

namespace LingEdu.Subscriptions.Domain.Subscriptions;

public class UserSubscription : Entity, IAuditable
{
    public Guid UserId { get; private set; }
    public Guid PlanId { get; private set; }
    public DateTime? ActiveUntil { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private UserSubscription()
    {
    }

    public UserSubscription(Guid userId, Guid planId, DateTime? activeUntil)
    {
        UserId = userId;
        PlanId = planId;
        ActiveUntil = activeUntil;
    }
}
