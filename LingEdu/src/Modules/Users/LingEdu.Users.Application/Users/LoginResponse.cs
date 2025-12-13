using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Users.Application.Users
{
    public sealed class LoginResponse
    {
        public string Token { get; init; } = default!;

        public UserDto User { get; init; } = default!;
    }
}
