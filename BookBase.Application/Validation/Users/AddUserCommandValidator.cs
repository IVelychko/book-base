using BookBase.Domain.Models.Commands.Users;
using FluentValidation;

namespace BookBase.Application.Validation.Users;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
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

        RuleFor(x => x.RoleIds)
            .NotEmpty()
            .WithMessage("You should provide at least one role id.")
            .Must(IsCollectionUnique)
            .WithMessage("Role Ids must be unique.")
            .DependentRules(() =>
            {
                RuleForEach(x => x.RoleIds)
                    .NotEmpty()
                    .WithMessage("RoleId cannot be empty.")
                    .Must(IsValidGuid)
                    .WithMessage("RoleId must be a valid GUID.");
            });
    }

    private static bool IsCollectionUnique(IEnumerable<string> roleIds)
    {
        HashSet<string> roleIdsSet = [.. roleIds];
        return roleIds.Count() == roleIdsSet.Count;
    }

    private static bool IsValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}