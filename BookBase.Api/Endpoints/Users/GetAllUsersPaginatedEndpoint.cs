using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Results;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Users;

public class GetAllUsersPaginatedEndpoint(IUserService userService)
    : Endpoint<GetAllUsersPaginatedRequest, PagedResult<UserDto>>
{
    public override void Configure()
    {
        Get("/api/users");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(GetAllUsersPaginatedRequest req, CancellationToken ct)
    {
        var result = await userService.GetAllUsersPaginatedAsync(req.PageNumber, req.PageSize);
        await SendOkAsync(result, ct);
    }
}