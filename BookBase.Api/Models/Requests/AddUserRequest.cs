namespace BookBase.Api.Models.Requests;

public record AddUserRequest(
    string Username,
    string? Email,
    string Password,
    IEnumerable<string> RoleIds
);