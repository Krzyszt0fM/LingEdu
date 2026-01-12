using LingEdu.Users.Domain.Enums;
using LingEdu.Users.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace LingEdu.Users.Application.Users
{
    internal static class UserMappingExtensions
    {
        public static UserDto ToDto(this User user)
        {
            string? uiLanguageCode = user.Language switch
            {
                Language.pl => "pl",
                Language.en => "en",
                Language.de => "de",
                _ => null
            };

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                DisplayName = null,
                AvatarUrl = null,
                CountryCode = null,
                UiLanguageCode = uiLanguageCode,
                IsActive = user.IsActive
            };
        }
    }
}

