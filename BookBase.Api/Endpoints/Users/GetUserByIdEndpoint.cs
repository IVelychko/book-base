using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Models.DTOs;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Users;

public class GetUserByIdEndpoint(IUserService userService) : Endpoint<GetUserByIdRequest, UserDto>
{
    public override void Configure()
    {
        Get("/api/users/{Id}");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(GetUserByIdRequest req, CancellationToken ct)
    {
        var result = await userService.GetUserByIdAsync(req.Id);
        await SendOkAsync(result, ct);
    }
}