using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Models.DTOs;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Auth;

public class SignUpUserEndpoint(IAuthService authService) : Endpoint<SignUpUserRequest, AuthorizedUserDto>
{
    public override void Configure()
    {
        Post("/api/auth/sign-up");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignUpUserRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        var result = await authService.SignUpUserAsync(command);
        await SendOkAsync(result, ct);
    }
}