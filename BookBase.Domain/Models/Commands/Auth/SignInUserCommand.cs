namespace BookBase.Domain.Models.Commands.Auth;

public record SignInUserCommand(
    string Username,
    string Password
);