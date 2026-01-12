using LingEdu.Subscriptions.Application.Services;
using LingEdu.Subscriptions.Domain.Repositories;
using LingEdu.Subscriptions.Infrastructure.Persistence;
using LingEdu.Subscriptions.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Subscriptions.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSubscriptionsModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SubscriptionsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Main")));

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            return services;
        }
    }
}