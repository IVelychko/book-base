using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Models.Commands.Auth;
using BookBase.Domain.Shared;
using FluentValidation;
using FluentValidation.Results;

namespace BookBase.Application.Validation.Auth;

public class AuthValidator(
    IValidator<SignUpUserCommand> signUpUserCommandValidator,
    IValidator<SignInUserCommand> signInUserCommandValidator
) : IAuthValidator
{
    public ValidationResult ValidateSignInUserCommand(SignInUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return signInUserCommandValidator.Validate(command);
    }

    public ValidationResult ValidateSignUpUserCommand(SignUpUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return signUpUserCommandValidator.Validate(command);
    }
}