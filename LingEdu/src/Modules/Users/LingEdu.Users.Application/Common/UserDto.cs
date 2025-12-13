using LingEdu.Users.Domain.Users;

namespace LingEdu.Users.Application.Common;

public record UserDto(Guid Id, string Email, string Login, Language Language, bool IsPremium);
