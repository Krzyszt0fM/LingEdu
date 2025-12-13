namespace LingEdu.Users.Application.Common
{
    public interface IPasswordHasher<TUser> where TUser : class
    {
        string HashPassword(TUser user, string password);
        bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword);
    }
}