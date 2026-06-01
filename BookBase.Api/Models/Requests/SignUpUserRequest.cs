namespace BookBase.Api.Models.Requests;

public record SignUpUserRequest(
    string Username,
    string? Email,
    string Password
);