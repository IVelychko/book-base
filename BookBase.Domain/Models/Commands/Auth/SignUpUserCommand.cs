namespace BookBase.Domain.Models.Commands.Auth;

public record SignUpUserCommand(
    string Username,
    string? Email,
    string Password
);