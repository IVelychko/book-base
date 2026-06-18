using BookBase.Application.Mapping.Extensions;
using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Models.Commands.Books;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Results;
using BookBase.Domain.Shared;

namespace BookBase.Application.Services;

public class BookService(
    IBookRepository bookRepository,
    IBookServiceValidator bookServiceValidator) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    private readonly IBookServiceValidator _bookServiceValidator = bookServiceValidator;

    public async Task<Guid> AddBookAsync(AddBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _bookServiceValidator.ValidateAddBookCommandAsync(command);

        var bookEntity = command.ToEntity();
        var bookId = await _bookRepository.AddBookAsync(bookEntity);
        return bookId;
    }

    public async Task DeleteBookAsync(DeleteBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _bookServiceValidator.ValidateDeleteBookCommandAsync(command);

        await _bookRepository.DeleteBookAsync(Guid.Parse(command.Id));
    }

    public async Task<PagedResult<BookDto>> GetAllBooksPaginatedAsync(int pageNumber, int pageSize)
    {
        var bookEntities = await _bookRepository.GetAllBooksPaginatedAsync(pageNumber, pageSize);
        var totalBooksCount = await _bookRepository.GetTotalBooksCountAsync();
        return new PagedResult<BookDto>
        {
            Items = bookEntities.Select(b => b.ToDto()).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItemsCount = totalBooksCount
        };
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
        var bookEntity = await _bookRepository.GetBookByIdAsync(id);
        Ensure.EntityExists(bookEntity, "Book with the specified ID does not exist.");
        return bookEntity.ToDto();
    }

    public async Task UpdateBookAsync(UpdateBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _bookServiceValidator.ValidateUpdateBookCommandAsync(command);

        var existingBook = await _bookRepository.GetBookByIdAsync(Guid.Parse(command.Id));
        Ensure.EntityExists(existingBook, "Book with the specified ID does not exist.");
        var bookEntity = command.ToEntity(existingBook.PublicationDate);
        await _bookRepository.UpdateBookAsync(bookEntity);
    }
}