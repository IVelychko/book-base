using BookBase.Api.Models.Requests;
using BookBase.Domain.Models.Commands.Auth;

namespace BookBase.Api.Mapping.Extensions;

public static class AuthMappingExtensions
{
    public static SignInUserCommand ToCommand(this SignInUserRequest request)
    {
        return new SignInUserCommand(
            request.Username,
            request.Password
        );
    }

    public static SignUpUserCommand ToCommand(this SignUpUserRequest request)
    {
        return new SignUpUserCommand(
            request.Username,
            request.Email,
            request.Password
        );
    }
}