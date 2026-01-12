using LingEdu.Contracts.Subscriptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LingEdu.Subscriptions.Application.Services
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionPlanContractDto>> GetPlansAsync(CancellationToken cancellationToken = default);
        Task<SubscriptionStatusContractDto> GetUserStatusAsync(Guid userId, CancellationToken cancellationToken = default);
        Task ActivateSubscriptionAsync(Guid userId, Guid planId, CancellationToken cancellationToken = default);
        Task<bool> HasAccessAsync(Guid userId, string featureName, CancellationToken cancellationToken = default);
    }
}