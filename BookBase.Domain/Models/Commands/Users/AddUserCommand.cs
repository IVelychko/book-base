namespace BookBase.Domain.Models.Commands.Users;

public record AddUserCommand(
    string Username,
    string? Email,
    string Password,
    IEnumerable<string> RoleIds
);