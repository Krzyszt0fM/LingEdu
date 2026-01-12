namespace LingEdu.Users.Domain.Users
{
    public sealed class Role
    {
        private Role()
        {
        }

        private Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public static Role Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Role name cannot be empty.", nameof(name));
            }

            return new Role(Guid.NewGuid(), name.Trim());
        }
    }
}
