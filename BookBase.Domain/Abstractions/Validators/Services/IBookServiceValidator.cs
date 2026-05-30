using BookBase.Domain.Models.Commands.Books;

namespace BookBase.Domain.Abstractions.Validators.Services;

public interface IBookServiceValidator
{
    Task ValidateAddBookCommandAsync(AddBookCommand command);

    Task ValidateUpdateBookCommandAsync(UpdateBookCommand command);

    Task ValidateDeleteBookCommandAsync(DeleteBookCommand command);
}