using LingEdu.Users.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace LingEdu.Users.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher<User>
{
    private readonly PasswordHasher<User> _inner = new();

    public string HashPassword(User user, string password) => _inner.HashPassword(user, password);

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword) =>
        _inner.VerifyHashedPassword(user, hashedPassword, providedPassword);
}
