namespace LingEdu.Users.Domain.Users
{
    public sealed class User
    {
        private User()
        {
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; } = default!;
        public string UserName { get; private set; } = default!;
        public string? DisplayName { get; private set; }
        public string? AvatarUrl { get; private set; }
        public string? CountryCode { get; private set; }
        public string? UiLanguageCode { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }
        public bool IsActive { get; private set; }

        public static User Create(string email, string userName, string? displayName, string? uiLanguageCode)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName is required.", nameof(userName));

            return new User
            {
                Id = Guid.NewGuid(),
                Email = email.Trim(),
                UserName = userName.Trim(),
                DisplayName = displayName?.Trim(),
                UiLanguageCode = uiLanguageCode,
                CreatedAtUtc = DateTime.UtcNow,
                IsActive = true
            };
        }

        public void UpdateProfile(string? displayName, string? avatarUrl, string? countryCode, string? uiLanguageCode)
        {
            DisplayName = displayName?.Trim();
            AvatarUrl = avatarUrl;
            CountryCode = countryCode;
            UiLanguageCode = uiLanguageCode;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
