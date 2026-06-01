namespace BookBase.Domain.Models.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<UserRoleEntity> UserRoles { get; set; } = [];
}