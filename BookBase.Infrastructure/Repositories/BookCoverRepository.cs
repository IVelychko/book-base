using BookBase.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class BookCoverRepository(BooksDbContext context) : IBookCoverRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<bool> BookCoverExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.BookCovers.AnyAsync(b => b.Id == id && b.Id != excludeId.Value);
        }

        return await _context.BookCovers.AnyAsync(b => b.Id == id);
    }
}