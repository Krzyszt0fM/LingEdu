using LingEdu.BuildingBlocks.Infrastructure;
using LingEdu.Exercises.Application.GetExercise;
using LingEdu.Exercises.Domain.Exercises;
using LingEdu.Exercises.Infrastructure.Persistence;
using LingEdu.Exercises.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Exercises.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddExercisesModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleInfrastructure<ExercisesDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Main");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseInMemoryDatabase("Exercises");
            }
            else
            {
                options.UseSqlServer(connectionString);
            }
        });

        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IProgressRepository, ProgressRepository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetExerciseQuery).Assembly));

        return services;
    }
}
