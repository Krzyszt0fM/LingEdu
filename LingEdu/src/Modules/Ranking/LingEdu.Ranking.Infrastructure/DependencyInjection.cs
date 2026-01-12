using LingEdu.Ranking.Application.Services;
using LingEdu.Ranking.Domain.Repositories;
using LingEdu.Ranking.Infrastructure.Persistence;
using LingEdu.Ranking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Ranking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRankingModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RankingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Main")));

            services.AddScoped<IRankingRepository, RankingRepository>();
            services.AddScoped<IRankingService, RankingService>();

            return services;
        }
    }
}
