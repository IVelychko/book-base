using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Models.Commands.Books;
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
        return addBookCommandValidator.Validate(command);
    }

    public ValidationResult ValidateUpdateBookCommand(UpdateBookCommand command)
    {
        return updateBookCommandValidator.Validate(command);
    }

    public ValidationResult ValidateDeleteBookCommand(DeleteBookCommand command)
    {
        return deleteBookCommandValidator.Validate(command);
    }
}