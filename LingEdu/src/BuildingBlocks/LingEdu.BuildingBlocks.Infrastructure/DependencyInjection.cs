using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddModuleInfrastructure<TContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder>? configureContext = null)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            if (configureContext is not null)
            {
                configureContext(options);
            }
            else
            {
                options.UseInMemoryDatabase(typeof(TContext).Name);
            }
        });

        return services;
    }
}
