using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Models.Commands.Users;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Users;

public class DeleteUserEndpoint(IUserService userService) : Endpoint<DeleteUserRequest>
{
    public override void Configure()
    {
        Delete("/api/users/{Id}");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        var command = new DeleteUserCommand(req.Id);
        await userService.DeleteUserAsync(command);
        await SendNoContentAsync(ct);
    }
}