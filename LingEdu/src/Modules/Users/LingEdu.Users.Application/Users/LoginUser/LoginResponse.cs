namespace LingEdu.Users.Application.Users.LoginUser;

public record LoginResponse(string Token, Guid UserId, string Email, string Login, bool IsPremium);
