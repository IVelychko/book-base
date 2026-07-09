using BookBase.Api.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace BookBase.Api.Validation.Users;

public class DeleteUserRequestValidator : Validator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
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