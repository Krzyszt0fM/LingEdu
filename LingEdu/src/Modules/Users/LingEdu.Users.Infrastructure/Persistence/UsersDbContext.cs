using LingEdu.BuildingBlocks.Infrastructure;
using LingEdu.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Users.Infrastructure.Persistence;

public class UsersDbContext : DbContextBase
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.RoleConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
