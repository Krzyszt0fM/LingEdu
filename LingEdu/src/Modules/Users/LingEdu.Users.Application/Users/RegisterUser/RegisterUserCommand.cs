using LingEdu.BuildingBlocks.Application;
using LingEdu.Users.Application.Common;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Users.Application.Users.RegisterUser;

public record RegisterUserCommand(string Email, string Login, string Password, Language Language) : ICommand<UserDto>;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            return Result<UserDto>.Failure(Error.Validation("Email already exists"));
        }

        var user = new User(request.Email, request.Login, string.Empty, request.Language);
        var passwordHash = _passwordHasher.HashPassword(user, request.Password);
        user = new User(request.Email, request.Login, passwordHash, request.Language);

        await _userRepository.AddAsync(user, cancellationToken);

        var dto = new UserDto(user.Id, user.Email, user.Login, user.Language, user.IsPremium);
        return Result<UserDto>.Success(dto);
    }
}
