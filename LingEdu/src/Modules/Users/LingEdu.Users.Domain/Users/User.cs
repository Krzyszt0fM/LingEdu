using LingEdu.Users.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LingEdu.Users.Domain.Users
{
    public sealed class User
    {
        private readonly List<Role> _roles = new();

        private User()
        {
        }

        private User(Guid id, string email, string userName, string passwordHash, Language language)
        {
            Id = id;
            Email = email;
            UserName = userName;
            PasswordHash = passwordHash;
            Language = language;
            IsPremium = false;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; } = default!;

        public string UserName { get; private set; } = default!;

        // hash – żadnych plain-text haseł
        public string PasswordHash { get; private set; } = default!;

        public Language Language { get; private set; }

        public bool IsPremium { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool IsActive { get; private set; }

        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

        public static User Create(string email, string userName, string passwordHash, Language language)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be empty.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
            }

            email = email.Trim();
            userName = userName.Trim();

            return new User(Guid.NewGuid(), email, userName, passwordHash, language);
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
            {
                throw new ArgumentException("Password hash cannot be empty.", nameof(newPasswordHash));
            }

            PasswordHash = newPasswordHash;
        }

        public void ChangeLanguage(Language language)
        {
            Language = language;
        }

        public void MarkAsPremium()
        {
            IsPremium = true;
        }

        public void MarkAsFree()
        {
            IsPremium = false;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void AddRole(Role role)
        {
            if (_roles.Any(r => r.Name == role.Name))
            {
                return;
            }

            _roles.Add(role);
        }

        public void RemoveRole(string roleName)
        {
            var existing = _roles.FirstOrDefault(r => r.Name == roleName);
            if (existing is null)
            {
                return;
            }

            _roles.Remove(existing);
        }
    }
}
