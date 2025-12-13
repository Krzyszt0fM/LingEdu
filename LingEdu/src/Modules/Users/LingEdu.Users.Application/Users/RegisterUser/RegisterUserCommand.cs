using LingEdu.BuildingBlocks.Application.Cqrs;

namespace LingEdu.Users.Application.Users.RegisterUser
{
    public sealed class RegisterUserCommand : ICommand<UserDto>
    {
        public RegisterUserCommand(string email, string userName, string password, int language)
        {
            Email = email;
            UserName = userName;
            Password = password;
            Language = language;
        }

        public string Email { get; }

        public string UserName { get; }

        public string Password { get; }

        public int Language { get; }
    }
}
