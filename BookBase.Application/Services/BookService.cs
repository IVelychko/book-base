using BookBase.Application.Extensions;
using BookBase.Application.Mapping.Extensions;
using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Books;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Results;
using BookBase.Domain.Shared;

namespace BookBase.Application.Services;

public class BookService(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IPublisherRepository publisherRepository,
    IBookTypeRepository bookTypeRepository,
    IBookCoverRepository bookCoverRepository,
    IBookValidator bookValidator) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IPublisherRepository _publisherRepository = publisherRepository;
    private readonly IBookTypeRepository _bookTypeRepository = bookTypeRepository;
    private readonly IBookCoverRepository _bookCoverRepository = bookCoverRepository;

    private readonly IBookValidator _bookValidator = bookValidator;

    public async Task<Guid> AddBookAsync(AddBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await ValidateAddBookCommandAsync(command);
        var bookEntity = command.ToEntity();
        await _bookRepository.AddBookAsync(bookEntity);
        return bookEntity.Id;
    }

    public async Task DeleteBookAsync(DeleteBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await ValidateDeleteBookCommandAsync(command);
        await _bookRepository.DeleteBookAsync(Guid.Parse(command.Id));
    }

    public async Task<PagedResult<Book>> GetAllBooksPaginatedAsync(int pageNumber, int pageSize)
    {
        var bookEntities = await _bookRepository.GetAllBooksPaginatedAsync(pageNumber, pageSize);
        var totalBooksCount = await _bookRepository.GetTotalBooksCountAsync();
        return new PagedResult<Book>
        {
            Items = bookEntities.Select(b => b.ToDto()).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItemsCount = totalBooksCount
        };
    }

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        var bookEntity = await _bookRepository.GetBookByIdAsync(id);
        bookEntity = Ensure.EntityExists(bookEntity, "Book with the specified ID does not exist.");
        return bookEntity.ToDto();
    }

    public async Task UpdateBookAsync(UpdateBookCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await ValidateUpdateBookCommandAsync(command);
        var existingBook = await _bookRepository.GetBookByIdAsync(Guid.Parse(command.Id));
        existingBook = Ensure.EntityExists(existingBook, "Book with the specified ID does not exist.");
        var bookEntity = command.ToEntity(existingBook.PublicationDate);
        await _bookRepository.UpdateBookAsync(bookEntity);
    }

    private async Task ValidateAddBookCommandAsync(AddBookCommand command)
    {
        var validationResult = _bookValidator.ValidateAddBookCommand(command);
        validationResult.ThrowIfValidationFailed();

        var validationErrors = new Dictionary<string, string[]>();
        await CheckBookNotExistsByTitleAndAuthorIdAsync(command.Title, Guid.Parse(command.AuthorId), validationErrors);
        await CheckAuthorExistsAsync(Guid.Parse(command.AuthorId), validationErrors);
        await CheckPublisherExistsAsync(Guid.Parse(command.PublisherId), validationErrors);
        await CheckBookTypeExistsAsync(Guid.Parse(command.BookTypeId), validationErrors);
        await CheckBookCoverExistsAsync(Guid.Parse(command.BookCoverId), validationErrors);
        ThrowIfErrorsExist(validationErrors);
    }

    private async Task ValidateUpdateBookCommandAsync(UpdateBookCommand command)
    {
        var validationResult = _bookValidator.ValidateUpdateBookCommand(command);
        validationResult.ThrowIfValidationFailed();
        await CheckBookExistsByIdAsync(Guid.Parse(command.Id));

        var validationErrors = new Dictionary<string, string[]>();
        await CheckBookNotExistsByTitleAndAuthorIdAsync(command.Title, Guid.Parse(command.AuthorId), validationErrors, Guid.Parse(command.Id));
        await CheckAuthorExistsAsync(Guid.Parse(command.AuthorId), validationErrors);
        await CheckPublisherExistsAsync(Guid.Parse(command.PublisherId), validationErrors);
        await CheckBookTypeExistsAsync(Guid.Parse(command.BookTypeId), validationErrors);
        await CheckBookCoverExistsAsync(Guid.Parse(command.BookCoverId), validationErrors);
        ThrowIfErrorsExist(validationErrors);
    }

    private async Task ValidateDeleteBookCommandAsync(DeleteBookCommand command)
    {
        var validationResult = _bookValidator.ValidateDeleteBookCommand(command);
        validationResult.ThrowIfValidationFailed();
        await CheckBookExistsByIdAsync(Guid.Parse(command.Id));
    }

    private async Task CheckBookExistsByIdAsync(Guid bookId)
    {
        var exists = await _bookRepository.BookExistsAsync(bookId);
        if (!exists)
        {
            throw new EntityNotFoundException("Book with the specified ID does not exist.");
        }
    }

    private async Task CheckBookNotExistsByTitleAndAuthorIdAsync(string title, Guid authorId, Dictionary<string, string[]> validationErrors, Guid? excludeBookId = null)
    {
        var exists = await _bookRepository.BookExistsByTitleAndAuthorIdAsync(title, authorId, excludeBookId);
        if (exists)
        {
            validationErrors.Add("titleAndAuthorId", ["Book with the specified title and author ID already exists."]);
        }
    }

    private async Task CheckAuthorExistsAsync(Guid authorId, Dictionary<string, string[]> validationErrors)
    {
        var exists = await _authorRepository.AuthorExistsAsync(authorId);
        if (!exists)
        {
            validationErrors.Add(nameof(authorId), ["Author with the specified ID does not exist."]);
        }
    }

    private async Task CheckPublisherExistsAsync(Guid publisherId, Dictionary<string, string[]> validationErrors)
    {
        var exists = await _publisherRepository.PublisherExistsAsync(publisherId);
        if (!exists)
        {
            validationErrors.Add(nameof(publisherId), ["Publisher with the specified ID does not exist."]);
        }
    }

    private async Task CheckBookTypeExistsAsync(Guid bookTypeId, Dictionary<string, string[]> validationErrors)
    {
        var exists = await _bookTypeRepository.BookTypeExistsAsync(bookTypeId);
        if (!exists)
        {
            validationErrors.Add(nameof(bookTypeId), ["Book type with the specified ID does not exist."]);
        }
    }

    private async Task CheckBookCoverExistsAsync(Guid bookCoverId, Dictionary<string, string[]> validationErrors)
    {
        var exists = await _bookCoverRepository.BookCoverExistsAsync(bookCoverId);
        if (!exists)
        {
            validationErrors.Add(nameof(bookCoverId), ["Book cover with the specified ID does not exist."]);
        }
    }

    private static void ThrowIfErrorsExist(Dictionary<string, string[]> validationErrors)
    {
        if (validationErrors.Count > 0)
        {
            throw new ValidationException(validationErrors);
        }
    }
}