using LingEdu.BuildingBlocks.Application;
using LingEdu.Subscriptions.Domain.Subscriptions;

namespace LingEdu.Subscriptions.Application.Status;

public record ActivateSubscriptionCommand(Guid UserId, Guid PlanId, DateTime? ActiveUntil) : ICommand;

public class ActivateSubscriptionCommandHandler : ICommandHandler<ActivateSubscriptionCommand>
{
    private readonly ISubscriptionRepository _repository;

    public ActivateSubscriptionCommandHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(ActivateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new UserSubscription(request.UserId, request.PlanId, request.ActiveUntil ?? DateTime.UtcNow.AddMonths(1));
        await _repository.SaveUserSubscriptionAsync(subscription, cancellationToken);
        return Result.Success();
    }
}
