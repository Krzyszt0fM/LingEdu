using System;
using LingEdu.BuildingBlocks.Application.Cqrs;

namespace LingEdu.Contests.Application.Contests.Join
{
    public sealed record JoinContestCommand(Guid UserId, Guid ContestId) : ICommand;
}
