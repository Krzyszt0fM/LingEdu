using LingEdu.Exercises.Application.Exercises.GetExercise;
using LingEdu.Exercises.Domain.Repositories;
using LingEdu.Exercises.Infrastructure.Persistence;
using LingEdu.Exercises.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Exercises.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExercisesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExercisesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Main")));

            services.AddScoped<IExerciseRepository, ExerciseRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetExerciseQuery).Assembly));

            return services;
        }
    }
}
