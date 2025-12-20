using LingEdu.Contracts.Subscriptions;
using LingEdu.Subscriptions.Domain;
using LingEdu.Subscriptions.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LingEdu.Subscriptions.Application.Services
{
    public sealed class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionService(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SubscriptionPlanContractDto>> GetPlansAsync(CancellationToken cancellationToken = default)
        {
            var plans = await _repository.GetAllPlansAsync(cancellationToken);

            return plans.Select(p => new SubscriptionPlanContractDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DurationInDays = p.DurationInDays
            }).ToList();
        }

        public async Task<SubscriptionStatusContractDto> GetUserStatusAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var userSub = await _repository.GetActiveByUserIdAsync(userId, cancellationToken);

            if (userSub is null)
            {
                return new SubscriptionStatusContractDto
                {
                    IsPremium = false,
                    ActiveTo = null,
                    CurrentPlan = null
                };
            }

            return new SubscriptionStatusContractDto
            {
                IsPremium = true,
                ActiveTo = userSub.ActiveTo,
                CurrentPlan = new SubscriptionPlanContractDto
                {
                    Id = userSub.Plan!.Id,
                    Name = userSub.Plan.Name,
                    Price = userSub.Plan.Price,
                    DurationInDays = userSub.Plan.DurationInDays
                }
            };
        }

        public async Task ActivateSubscriptionAsync(Guid userId, Guid planId, CancellationToken cancellationToken = default)
        {
            var plan = await _repository.GetPlanByIdAsync(planId, cancellationToken);
            if (plan is null)
            {
                throw new Exception($"Plan with id {planId} not found");
            }

            var activeFrom = DateTime.UtcNow;
            var activeTo = activeFrom.AddDays(plan.DurationInDays);

            var subscription = new UserSubscription(Guid.NewGuid(), userId, planId, activeFrom, activeTo);

            await _repository.AddAsync(subscription, cancellationToken);
        }

        public async Task<bool> HasAccessAsync(Guid userId, string featureName, CancellationToken cancellationToken = default)
        {
            var sub = await _repository.GetActiveByUserIdAsync(userId, cancellationToken);
            return sub is not null;
        }
    }
}