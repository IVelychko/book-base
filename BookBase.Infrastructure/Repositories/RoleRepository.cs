using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Models.Entities;
using BookBase.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class RoleRepository(BooksDbContext context) : IRoleRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<RoleEntity?> GetRoleByNameAsync(string name)
    {
        Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<bool> RoleExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.Roles.AnyAsync(r => r.Id == id && r.Id != excludeId.Value);
        }

        return await _context.Roles.AnyAsync(r => r.Id == id);
    }
}