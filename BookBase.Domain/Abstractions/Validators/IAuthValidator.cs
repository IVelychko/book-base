using BookBase.Domain.Models.Commands.Auth;
using FluentValidation.Results;

namespace BookBase.Domain.Abstractions.Validators;

public interface IAuthValidator
{
    ValidationResult ValidateSignUpUserCommand(SignUpUserCommand command);

    ValidationResult ValidateSignInUserCommand(SignInUserCommand command);
}