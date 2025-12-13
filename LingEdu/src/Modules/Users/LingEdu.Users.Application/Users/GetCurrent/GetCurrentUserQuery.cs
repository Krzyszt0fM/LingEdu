using LingEdu.BuildingBlocks.Application;
using LingEdu.Users.Application.Common;
using LingEdu.Users.Domain.Users;

namespace LingEdu.Users.Application.Users.GetCurrent;

public record GetCurrentUserQuery(Guid UserId) : IQuery<UserDto>;

public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure(Error.NotFound("User not found"));
        }

        var dto = new UserDto(user.Id, user.Email, user.Login, user.Language, user.IsPremium);
        return Result<UserDto>.Success(dto);
    }
}
