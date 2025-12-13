using LingEdu.BuildingBlocks.Application;
using LingEdu.Users.Application.Auth;

namespace LingEdu.Users.Application.Users.LoginUser;

public record LoginUserQuery(string Email, string Password) : IQuery<LoginResponse>;

public class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginUserQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<Result<LoginResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var loginRequest = new LoginRequest(request.Email, request.Password);
        return _authService.LoginAsync(loginRequest, cancellationToken);
    }
}
