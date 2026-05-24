using BookBase.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class PublisherRepository(BooksDbContext context) : IPublisherRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<bool> PublisherExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id && p.Id != excludeId.Value);
        }

        return await _context.Publishers.AnyAsync(p => p.Id == id);
    }
}