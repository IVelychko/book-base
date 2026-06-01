namespace BookBase.Domain.Models.DTOs;

public class UserRole
{
    public Guid RoleId { get; set; }

    public string Role { get; set; } = string.Empty;
}