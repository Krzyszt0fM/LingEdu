namespace LingEdu.Users.Application.Users
{
    public sealed class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CountryCode { get; set; }
        public string? UiLanguageCode { get; set; }
        public bool IsActive { get; set; }
    }
}
