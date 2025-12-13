using LingEdu.Users.Domain.Users;
using LingEdu.Users.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Users.Infrastructure.Persistence
{
    public sealed class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
