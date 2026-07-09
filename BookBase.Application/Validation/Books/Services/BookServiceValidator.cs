using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Books;

namespace BookBase.Application.Validation.Books.Services;

public class BookServiceValidator(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IPublisherRepository publisherRepository,
    IBookTypeRepository bookTypeRepository,
    IBookCoverRepository bookCoverRepository) : IBookServiceValidator
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IPublisherRepository _publisherRepository = publisherRepository;
    private readonly IBookTypeRepository _bookTypeRepository = bookTypeRepository;
    private readonly IBookCoverRepository _bookCoverRepository = bookCoverRepository;

    public async Task ValidateAddBookCommandAsync(AddBookCommand command)
    {
        var validationErrors = new Dictionary<string, string[]>();
        await CheckBookNotExistsByTitleAndAuthorIdAsync(command.Title, Guid.Parse(command.AuthorId), validationErrors);
        await CheckAuthorExistsAsync(Guid.Parse(command.AuthorId), validationErrors);
        await CheckPublisherExistsAsync(Guid.Parse(command.PublisherId), validationErrors);
        await CheckBookTypeExistsAsync(Guid.Parse(command.BookTypeId), validationErrors);
        await CheckBookCoverExistsAsync(Guid.Parse(command.BookCoverId), validationErrors);
        ValidationHelper.ThrowIfErrorsExist(validationErrors);
    }

    public async Task ValidateUpdateBookCommandAsync(UpdateBookCommand command)
    {
        await CheckBookExistsByIdAsync(Guid.Parse(command.Id));

        var validationErrors = new Dictionary<string, string[]>();
        await CheckBookNotExistsByTitleAndAuthorIdAsync(command.Title, Guid.Parse(command.AuthorId), validationErrors, Guid.Parse(command.Id));
        await CheckAuthorExistsAsync(Guid.Parse(command.AuthorId), validationErrors);
        await CheckPublisherExistsAsync(Guid.Parse(command.PublisherId), validationErrors);
        await CheckBookTypeExistsAsync(Guid.Parse(command.BookTypeId), validationErrors);
        await CheckBookCoverExistsAsync(Guid.Parse(command.BookCoverId), validationErrors);
        ValidationHelper.ThrowIfErrorsExist(validationErrors);
    }

    public async Task ValidateDeleteBookCommandAsync(DeleteBookCommand command)
    {
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
}