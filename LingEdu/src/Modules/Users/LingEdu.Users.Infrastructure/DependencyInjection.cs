using LingEdu.BuildingBlocks.Infrastructure;
using LingEdu.Users.Application.Auth;
using LingEdu.Users.Application.Users.RegisterUser;
using LingEdu.Users.Domain.Users;
using LingEdu.Users.Infrastructure.Persistence;
using LingEdu.Users.Infrastructure.Persistence.Repositories;
using LingEdu.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LingEdu.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddModuleInfrastructure<UsersDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Main");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseInMemoryDatabase("Users");
            }
            else
            {
                options.UseSqlServer(connectionString);
            }
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

        return services;
    }
}
