using BookBase.Domain.Models.Commands.Auth;
using BookBase.Domain.Models.DTOs;

namespace BookBase.Domain.Abstractions.Services;

public interface IAuthService
{
    Task<AuthorizedUserDto> SignInUserAsync(SignInUserCommand command);

    Task<AuthorizedUserDto> SignUpUserAsync(SignUpUserCommand command);
}