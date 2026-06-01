using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Models.Commands.Books;
using BookBase.Domain.Shared;
using FluentValidation;
using FluentValidation.Results;

namespace BookBase.Application.Validation.Books;

public class BookValidator(
    IValidator<AddBookCommand> addBookCommandValidator,
    IValidator<UpdateBookCommand> updateBookCommandValidator,
    IValidator<DeleteBookCommand> deleteBookCommandValidator
) : IBookValidator
{
    public ValidationResult ValidateAddBookCommand(AddBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return addBookCommandValidator.Validate(command);
    }

    public ValidationResult ValidateUpdateBookCommand(UpdateBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return updateBookCommandValidator.Validate(command);
    }

    public ValidationResult ValidateDeleteBookCommand(DeleteBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return deleteBookCommandValidator.Validate(command);
    }
}