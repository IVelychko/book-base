using BookBase.Domain.Models.Commands.Users;
using FluentValidation;

namespace BookBase.Application.Validation.Users;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(IsValidGuid)
            .WithMessage("RoleId must be a valid GUID.");
    }

    private static bool IsValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}