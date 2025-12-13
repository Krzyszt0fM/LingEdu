namespace LingEdu.Contracts.Subscriptions;

public record SubscriptionStatusDto(string PlanName, DateTime? ActiveUntil, bool IsActive);

public record PlanDto(Guid Id, string Name, decimal Price, string Period);
