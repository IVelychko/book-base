using BookBase.Domain.Models.Entities;

namespace BookBase.Domain.Abstractions.Repositories;

public interface IBookRepository
{
    Task<IList<BookEntity>> GetAllBooksPaginatedAsync(int pageNumber, int pageSize);

    Task<BookEntity?> GetBookByIdAsync(Guid id);

    Task<Guid> AddBookAsync(BookEntity book);

    Task UpdateBookAsync(BookEntity book);

    Task DeleteBookAsync(Guid id);

    Task<bool> BookExistsAsync(Guid id, Guid? excludeId = null);

    Task<bool> BookExistsByTitleAndAuthorIdAsync(string title, Guid authorId, Guid? excludeBookId = null);

    Task<int> GetTotalBooksCountAsync();
}