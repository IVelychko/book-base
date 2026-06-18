namespace BookBase.Domain.Models.DTOs;

public class UserRoleDto
{
    public Guid RoleId { get; set; }

    public string Role { get; set; } = string.Empty;
}