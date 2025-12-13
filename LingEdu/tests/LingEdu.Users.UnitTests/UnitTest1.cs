using LingEdu.Users.Application.Users.RegisterUser;
using LingEdu.Users.Domain.Users;
using LingEdu.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace LingEdu.Users.UnitTests;

public class UnitTest1
{
    [Fact]
    public void PasswordHasher_HashesValue()
    {
        var hasher = new PasswordHasher();
        var user = new User("test@example.com", "test", string.Empty, Language.English);
        var hash = hasher.HashPassword(user, "password");

        Assert.NotEqual("password", hash);
        Assert.NotEqual(PasswordVerificationResult.Failed, hasher.VerifyHashedPassword(user, hash, "password"));
    }
}
