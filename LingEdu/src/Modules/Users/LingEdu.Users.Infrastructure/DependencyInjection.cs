using LingEdu.Users.Infrastructure.Persistence;
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
        //services.AddDbContext<UsersDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("Users")));

        //services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
