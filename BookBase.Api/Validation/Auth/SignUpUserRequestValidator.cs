using BookBase.Api.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace BookBase.Api.Validation.Auth;

public class SignUpUserRequestValidator : Validator<SignUpUserRequest>
{
    public SignUpUserRequestValidator()
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