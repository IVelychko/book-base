using BookBase.Domain.Models.Commands.Books;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Results;

namespace BookBase.Domain.Abstractions.Services;

public interface IBookService
{
    Task<PagedResult<Book>> GetAllBooksPaginatedAsync(int pageNumber, int pageSize);

    Task<Book> GetBookByIdAsync(Guid id);

    Task<Guid> AddBookAsync(AddBookCommand command);

    Task UpdateBookAsync(UpdateBookCommand command);

    Task DeleteBookAsync(DeleteBookCommand command);
}