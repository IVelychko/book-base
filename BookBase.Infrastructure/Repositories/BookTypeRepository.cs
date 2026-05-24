using BookBase.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class BookTypeRepository(BooksDbContext context) : IBookTypeRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<bool> BookTypeExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.BookTypes.AnyAsync(a => a.Id == id && a.Id != excludeId.Value);
        }

        return await _context.BookTypes.AnyAsync(a => a.Id == id);
    }
}