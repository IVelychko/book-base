using BookBase.Domain.Models.Commands.Books;
using FluentValidation.Results;

namespace BookBase.Domain.Abstractions.Validators;

public interface IBookValidator
{
    ValidationResult ValidateAddBookCommand(AddBookCommand command);

    ValidationResult ValidateUpdateBookCommand(UpdateBookCommand command);

    ValidationResult ValidateDeleteBookCommand(DeleteBookCommand command);
}