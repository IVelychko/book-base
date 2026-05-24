using BookBase.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class AuthorRepository(BooksDbContext context) : IAuthorRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<bool> AuthorExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id && a.Id != excludeId.Value);
        }

        return await _context.Authors.AnyAsync(a => a.Id == id);
    }
}