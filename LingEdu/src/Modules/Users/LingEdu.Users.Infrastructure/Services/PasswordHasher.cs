using LingEdu.Users.Application.Common;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Users.Infrastructure.Services
{
    internal sealed class PasswordHasher : IPasswordHasher<User>
    {
        private readonly Microsoft.AspNetCore.Identity.PasswordHasher<User> _hasher = new();

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);

            return result != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed;
        }
    }
}