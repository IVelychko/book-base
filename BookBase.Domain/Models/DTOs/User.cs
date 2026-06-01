namespace BookBase.Domain.Models.DTOs;

public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Email { get; set; }

    public IEnumerable<UserRole> UserRoles { get; set; } = [];
}