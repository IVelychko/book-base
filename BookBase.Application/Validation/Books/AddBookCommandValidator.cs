using BookBase.Domain.Models.Commands.Books;
using FluentValidation;

namespace BookBase.Application.Validation.Books;

public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required.")
            .Must(IsValidGuid)
            .WithMessage("AuthorId must be a valid GUID.");

        RuleFor(x => x.BookCoverId)
            .NotEmpty()
            .WithMessage("BookCoverId is required.")
            .Must(IsValidGuid)
            .WithMessage("BookCoverId must be a valid GUID.");

        RuleFor(x => x.BookTypeId)
            .NotEmpty()
            .WithMessage("BookTypeId is required.")
            .Must(IsValidGuid)
            .WithMessage("BookTypeId must be a valid GUID.");

        RuleFor(x => x.PublisherId)
            .NotEmpty()
            .WithMessage("PublisherId is required.")
            .Must(IsValidGuid)
            .WithMessage("PublisherId must be a valid GUID.");
    }

    private static bool IsValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}