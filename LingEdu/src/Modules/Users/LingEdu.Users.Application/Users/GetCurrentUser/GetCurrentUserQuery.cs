using LingEdu.BuildingBlocks.Application.Cqrs;

namespace LingEdu.Users.Application.Users.GetCurrentUser
{
    public sealed record GetCurrentUserQuery(Guid UserId) : IQuery<UserDto>;
}