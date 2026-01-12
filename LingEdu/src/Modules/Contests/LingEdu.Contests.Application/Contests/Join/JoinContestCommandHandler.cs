using System;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contests.Application.Common;
using LingEdu.Contests.Domain.Repositories;

namespace LingEdu.Contests.Application.Contests.Join
{
    internal sealed class JoinContestCommandHandler : ICommandHandler<JoinContestCommand>
    {
        private readonly IContestRepository _contestRepository;

        public JoinContestCommandHandler(IContestRepository contestRepository)
        {
            _contestRepository = contestRepository;
        }

        public async Task<Result> Handle(JoinContestCommand request, CancellationToken cancellationToken)
        {
            var contest = await _contestRepository.GetByIdAsync(request.ContestId, cancellationToken);
            if (contest is null)
            {
                return Result.Failure(ContestErrors.ContestNotFound);
            }

            if (!contest.IsActive(DateTime.UtcNow))
            {
                return Result.Failure(ContestErrors.ContestNotActive);
            }

            if (await _contestRepository.IsJoinedAsync(request.ContestId, request.UserId, cancellationToken))
            {
                return Result.Failure(ContestErrors.AlreadyJoined);
            }

            await _contestRepository.JoinAsync(request.ContestId, request.UserId, DateTime.UtcNow, cancellationToken);

            return Result.Success();
        }
    }
}
