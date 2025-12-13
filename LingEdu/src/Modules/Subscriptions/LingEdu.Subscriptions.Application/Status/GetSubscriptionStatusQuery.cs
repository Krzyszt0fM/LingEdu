using LingEdu.BuildingBlocks.Application;
using LingEdu.Contracts.Subscriptions;
using LingEdu.Subscriptions.Domain.Subscriptions;

namespace LingEdu.Subscriptions.Application.Status;

public record GetSubscriptionStatusQuery(Guid UserId) : IQuery<SubscriptionStatusDto>;

public class GetSubscriptionStatusQueryHandler : IQueryHandler<GetSubscriptionStatusQuery, SubscriptionStatusDto>
{
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionStatusQueryHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<SubscriptionStatusDto>> Handle(GetSubscriptionStatusQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (subscription is null)
        {
            return Result<SubscriptionStatusDto>.Success(new SubscriptionStatusDto("Free", null, false));
        }

        var plan = (await _repository.GetPlansAsync(cancellationToken)).FirstOrDefault(p => p.Id == subscription.PlanId);
        var dto = new SubscriptionStatusDto(plan?.Name ?? "Unknown", subscription.ActiveUntil, subscription.ActiveUntil >= DateTime.UtcNow);
        return Result<SubscriptionStatusDto>.Success(dto);
    }
}
