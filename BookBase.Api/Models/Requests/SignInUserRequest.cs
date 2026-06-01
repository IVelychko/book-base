namespace BookBase.Api.Models.Requests;

public record SignInUserRequest(
    string Username,
    string Password
);