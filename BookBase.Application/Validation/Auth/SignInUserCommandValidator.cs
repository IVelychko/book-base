using BookBase.Domain.Models.Commands.Auth;
using FluentValidation;

namespace BookBase.Application.Validation.Auth;

public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
{
    public SignInUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}