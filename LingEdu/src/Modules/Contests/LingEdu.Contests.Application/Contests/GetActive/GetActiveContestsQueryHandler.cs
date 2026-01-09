using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Contests;
using LingEdu.Contests.Domain.Repositories;

namespace LingEdu.Contests.Application.Contests.GetActive
{
    internal sealed class GetActiveContestsQueryHandler
        : IQueryHandler<GetActiveContestsQuery, List<ContestContractDto>>
    {
        private readonly IContestRepository _contestRepository;

        public GetActiveContestsQueryHandler(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository;
        }

        public async Task<Result<List<ContestContractDto>>> Handle(GetActiveContestsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var contests = await _contestRepository.GetActiveAsync(now, cancellationToken);

            var contestIds = contests.Select(c => c.Id).ToList();
            var joinedIds = await _contestRepository.GetJoinedContestIdsAsync(request.UserId, contestIds, cancellationToken);

            var result = contests.Select(c => new ContestContractDto
            {
                Id = c.Id,
                Name = c.Name,
                StartsAt = c.StartsAt,
                EndsAt = c.EndsAt,
                IsJoined = joinedIds.Contains(c.Id)
            }).ToList();

            return Result<List<ContestContractDto>>.Success(result);
        }
    }
}
