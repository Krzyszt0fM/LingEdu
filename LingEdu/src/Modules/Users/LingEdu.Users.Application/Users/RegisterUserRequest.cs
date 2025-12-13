using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Users.Application.Users
{
    public sealed class RegisterUserRequest
    {
        public string Email { get; init; } = default!;

        public string UserName { get; init; } = default!;

        public string Password { get; init; } = default!;

        public int Language { get; init; }
    }
}
