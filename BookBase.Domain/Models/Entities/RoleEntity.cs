namespace BookBase.Domain.Models.Entities;

public class RoleEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<UserRoleEntity> UserRoles { get; set; } = [];
}