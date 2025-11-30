using LingEdu.Users.Domain.Users;
using LingEdu.Users.Infrastructure.Persistence;
using LingEdu.Users.Infrastructure.Persistence.Repositories;
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
        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Main")));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
