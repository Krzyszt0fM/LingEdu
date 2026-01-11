using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Reports;
using LingEdu.Exercises.Domain.Repositories;
using LingEdu.Ranking.Application.Services;

namespace LingEdu.Reports.Application.Reports.GetMyReport
{
    internal sealed class GetMyReportQueryHandler
        : IQueryHandler<GetMyReportQuery, UserReportContractDto>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IRankingService _rankingService;

        public GetMyReportQueryHandler(IExerciseRepository exerciseRepository, IRankingService rankingService)
        {
            _exerciseRepository = exerciseRepository;
            _rankingService = rankingService;
        }

        public async Task<Result<UserReportContractDto>> Handle(GetMyReportQuery request, CancellationToken cancellationToken)
        {
            var progress = await _exerciseRepository.GetUserProgressAsync(request.UserId, cancellationToken);

            var score = await _rankingService.GetUserScoreAsync(request.UserId, cancellationToken);

            var dto = new UserReportContractDto
            {
                UserId = request.UserId,
                TotalPoints = score.TotalPoints,
                CompletedExercisesCount = progress.Count,
                LastCompletedAt = progress.Count == 0 ? null : progress.Max(p => p.CompletedAt),
                RecentProgress = progress
                    .Take(20)
                    .Select(p => new UserProgressEntryContractDto
                    {
                        ExerciseId = p.ExerciseId,
                        Points = p.Points,
                        IsCorrect = p.IsCorrect,
                        CompletedAt = p.CompletedAt
                    })
                    .ToList()
            };

            return Result<UserReportContractDto>.Success(dto);
        }
    }
}
