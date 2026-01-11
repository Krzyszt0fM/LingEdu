using System;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Contracts.Reports;

namespace LingEdu.Reports.Application.Reports.GetMyReport
{
    public sealed record GetMyReportQuery(Guid UserId) : IQuery<UserReportContractDto>;
}
