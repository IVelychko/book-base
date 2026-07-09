using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Users;

public class AddUserEndpoint(IUserService userService) : Endpoint<AddUserRequest>
{
    public override void Configure()
    {
        Post("/api/users");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(AddUserRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        var result = await userService.AddUserAsync(command);
        await SendCreatedAtAsync<GetUserByIdEndpoint>(new { Id = result }, null, cancellation: ct);
    }
}