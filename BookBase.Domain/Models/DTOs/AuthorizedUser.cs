namespace BookBase.Domain.Models.DTOs;

public class AuthorizedUser
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string AccessToken { get; set; } = string.Empty;

    public IEnumerable<UserRole> UserRoles { get; set; } = [];
}