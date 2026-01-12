using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Contracts.Users
{
    public sealed class UserContractDto
    {
        public Guid Id { get; init; }

        public string Email { get; init; } = default!;

        public string UserName { get; init; } = default!;

        public bool IsPremium { get; init; }

        public string Language { get; init; } = "en";
    }
}
