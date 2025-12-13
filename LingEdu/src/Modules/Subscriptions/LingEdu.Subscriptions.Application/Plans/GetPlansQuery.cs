using LingEdu.BuildingBlocks.Application;
using LingEdu.Contracts.Subscriptions;
using LingEdu.Subscriptions.Domain.Subscriptions;

namespace LingEdu.Subscriptions.Application.Plans;

public record GetPlansQuery() : IQuery<List<PlanDto>>;

public class GetPlansQueryHandler : IQueryHandler<GetPlansQuery, List<PlanDto>>
{
    private readonly ISubscriptionRepository _repository;

    public GetPlansQueryHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<PlanDto>>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await _repository.GetPlansAsync(cancellationToken);
        var dto = plans.Select(p => new PlanDto(p.Id, p.Name, p.Price, p.Period)).ToList();
        return Result<List<PlanDto>>.Success(dto);
    }
}
