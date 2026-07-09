using BookBase.Api.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace BookBase.Api.Validation.Auth;

public class SignInUserRequestValidator : Validator<SignInUserRequest>
{
    public SignInUserRequestValidator()
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