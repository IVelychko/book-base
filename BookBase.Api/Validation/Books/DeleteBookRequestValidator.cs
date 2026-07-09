using BookBase.Api.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace BookBase.Api.Validation.Books;

public class DeleteBookRequestValidator : Validator<DeleteBookRequest>
{
    public DeleteBookRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(IsValidGuid)
            .WithMessage("Id must be a valid GUID.");
    }

    private static bool IsValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}