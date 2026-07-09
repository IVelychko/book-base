using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Models.DTOs;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Auth;

public class SignInUserEndpoint(IAuthService authService) : Endpoint<SignInUserRequest, AuthorizedUserDto>
{
    public override void Configure()
    {
        Post("/api/auth/sign-in");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignInUserRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        var result = await authService.SignInUserAsync(command);
        await SendOkAsync(result, ct);
    }
}