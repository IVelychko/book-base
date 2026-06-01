using BookBase.Domain.Models.Commands.Auth;
using BookBase.Domain.Models.DTOs;

namespace BookBase.Domain.Abstractions.Services;

public interface IAuthService
{
    Task<AuthorizedUser> SignInUserAsync(SignInUserCommand command);

    Task<AuthorizedUser> SignUpUserAsync(SignUpUserCommand command);
}