using LingEdu.Reports.Application.Reports.GetMyReport;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Reports.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReportsModule(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMyReportQuery).Assembly));
            return services;
        }
    }
}
