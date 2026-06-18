namespace BookBase.Domain.Models.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Email { get; set; }

    public IEnumerable<UserRoleDto> UserRoles { get; set; } = [];
}