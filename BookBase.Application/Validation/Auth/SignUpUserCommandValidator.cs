using BookBase.Domain.Models.Commands.Auth;
using FluentValidation;

namespace BookBase.Application.Validation.Auth;

public class SignUpUserCommandValidator : AbstractValidator<SignUpUserCommand>
{
    public SignUpUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .MaximumLength(50)
            .WithMessage("Username must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email must be a valid email address.")
            .MaximumLength(100)
            .WithMessage("Email must not exceed 100 characters.")
            .When(x => x.Email is not null);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");
    }
}