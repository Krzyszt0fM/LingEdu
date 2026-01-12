using System;
using System.Collections.Generic;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Contests;

namespace LingEdu.Contests.Application.Contests.GetActive
{
    public sealed record GetActiveContestsQuery(Guid UserId) : IQuery<List<ContestContractDto>>;
}
