using LingEdu.Contests.Application.Contests.GetActive;
using LingEdu.Contests.Domain.Repositories;
using LingEdu.Contests.Infrastructure.Persistence;
using LingEdu.Contests.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Contests.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContestsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContestsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Main")));

            services.AddScoped<IContestRepository, ContestRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetActiveContestsQuery).Assembly));

            return services;
        }
    }
}
