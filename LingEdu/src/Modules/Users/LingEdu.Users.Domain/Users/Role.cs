using LingEdu.BuildingBlocks.Domain;

namespace LingEdu.Users.Domain.Users;

public class Role : Entity
{
    public string Name { get; private set; }

    private Role()
    {
        Name = string.Empty;
    }

    public Role(string name)
    {
        Name = name;
    }
}
