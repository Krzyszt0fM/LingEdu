using LingEdu.BuildingBlocks.Application.Cqrs;

namespace LingEdu.Users.Application.Users.LoginUser
{
    public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginResponse>;
}