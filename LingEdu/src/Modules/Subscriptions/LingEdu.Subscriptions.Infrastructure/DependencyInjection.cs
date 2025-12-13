using LingEdu.BuildingBlocks.Infrastructure;
using LingEdu.Subscriptions.Application.Plans;
using LingEdu.Subscriptions.Application.Status;
using LingEdu.Subscriptions.Domain.Subscriptions;
using LingEdu.Subscriptions.Infrastructure.Persistence;
using LingEdu.Subscriptions.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Subscriptions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSubscriptionsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleInfrastructure<SubscriptionsDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Main");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseInMemoryDatabase("Subscriptions");
            }
            else
            {
                options.UseSqlServer(connectionString);
            }
        });

        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPlansQuery).Assembly));

        return services;
    }
}
