using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Entities;
using BookBase.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class BookRepository(BooksDbContext context) : IBookRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<Guid> AddBookAsync(BookEntity book)
    {
        Ensure.ArgumentNotNull(book);
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book.Id;
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new EntityNotFoundException("Book with the specified ID does not exist.");
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<BookEntity>> GetAllBooksPaginatedAsync(int pageNumber, int pageSize)
    {
        var itemsToSkip = (pageNumber - 1) * pageSize;
        return await _context.Books
            .OrderBy(b => b.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookType)
            .Include(b => b.BookCover)
            .ToListAsync();
    }

    public async Task<BookEntity?> GetBookByIdAsync(Guid id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookType)
            .Include(b => b.BookCover)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task UpdateBookAsync(BookEntity book)
    {
        Ensure.ArgumentNotNull(book);
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> BookExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.Books.AnyAsync(b => b.Id == id && b.Id != excludeId.Value);
        }

        return await _context.Books.AnyAsync(b => b.Id == id);
    }

    public async Task<bool> BookExistsByTitleAndAuthorIdAsync(string title, Guid authorId, Guid? excludeBookId = null)
    {
        Ensure.ArgumentNotNullOrWhiteSpace(title, nameof(title));
        if (excludeBookId.HasValue)
        {
            return await _context.Books.AnyAsync(b => b.Title == title && b.AuthorId == authorId && b.Id != excludeBookId.Value);
        }

        return await _context.Books.AnyAsync(b => b.Title == title && b.AuthorId == authorId);
    }

    public async Task<int> GetTotalBooksCountAsync()
    {
        return await _context.Books.CountAsync();
    }
}