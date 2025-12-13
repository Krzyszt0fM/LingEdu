using LingEdu.BuildingBlocks.Application;
using LingEdu.Users.Application.Users.LoginUser;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Users.Application.Auth;

public interface IAuthService
{
    Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
