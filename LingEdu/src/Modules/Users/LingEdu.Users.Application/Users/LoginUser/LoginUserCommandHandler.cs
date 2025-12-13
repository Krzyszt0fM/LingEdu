using LingEdu.BuildingBlocks.Application.Common;
using LingEdu.BuildingBlocks.Application.Cqrs;
using LingEdu.Users.Application.Auth;
using LingEdu.Users.Application.Common;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Users.Application.Users.LoginUser
{
    internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null)
            {
                return Result<LoginResponse>.Failure("InvalidCredentials");
            }

            var isValid = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (!isValid)
            {
                return Result<LoginResponse>.Failure("InvalidCredentials");
            }

            if (!user.IsActive)
            {
                return Result<LoginResponse>.Failure("UserNotActive");
            }

            var userDto = user.ToDto();
            var token = _authService.GenerateToken(userDto);

            return Result<LoginResponse>.Success(new LoginResponse
            {
                Token = token,
                User = userDto
            });
        }
    }
}