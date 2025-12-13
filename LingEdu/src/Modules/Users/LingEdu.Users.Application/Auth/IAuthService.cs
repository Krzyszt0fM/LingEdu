using LingEdu.Users.Application.Users;

namespace LingEdu.Users.Application.Auth
{
    public interface IAuthService
    {
        string GenerateToken(UserDto user);
    }
}
