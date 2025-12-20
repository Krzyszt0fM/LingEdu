using LingEdu.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LingEdu.Users.Infrastructure.Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }


        public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
