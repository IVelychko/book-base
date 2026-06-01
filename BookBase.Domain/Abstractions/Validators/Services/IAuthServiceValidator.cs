using BookBase.Domain.Models.Commands.Auth;

namespace BookBase.Domain.Abstractions.Validators.Services;

public interface IAuthServiceValidator
{
    Task ValidateSignUpUserCommandAsync(SignUpUserCommand command);

    Task ValidateSignInUserCommandAsync(SignInUserCommand command);
}