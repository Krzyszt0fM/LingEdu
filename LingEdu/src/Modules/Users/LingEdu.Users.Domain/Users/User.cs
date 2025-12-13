using LingEdu.BuildingBlocks.Domain;

namespace LingEdu.Users.Domain.Users;

public class User : AggregateRoot, IAuditable
{
    private readonly List<Role> _roles = new();

    public string Email { get; private set; }
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    public Language Language { get; private set; }
    public bool IsPremium { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private User()
    {
        Email = string.Empty;
        Login = string.Empty;
        PasswordHash = string.Empty;
    }

    public User(string email, string login, string passwordHash, Language language)
    {
        Email = email;
        Login = login;
        PasswordHash = passwordHash;
        Language = language;
    }

    public void PromoteToPremium() => IsPremium = true;

    public void UpdateLanguage(Language language) => Language = language;
}
