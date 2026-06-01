using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Entities;
using BookBase.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure.Repositories;

public class UserRepository(BooksDbContext context) : IUserRepository
{
    private readonly BooksDbContext _context = context;

    public async Task<Guid> AddUserAsync(UserEntity user)
    {
        Ensure.ArgumentNotNull(user);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new EntityNotFoundException("User with the specified ID does not exist.");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<UserEntity>> GetAllUsersPaginatedAsync(int pageNumber, int pageSize)
    {
        var itemsToSkip = (pageNumber - 1) * pageSize;
        return await _context.Users
            .OrderBy(u => u.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<int> GetTotalUsersCountAsync()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<UserEntity?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserEntity?> GetUserByUsernameAsync(string username)
    {
        Ensure.ArgumentNotNullOrWhiteSpace(username, nameof(username));
        return await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        Ensure.ArgumentNotNull(user);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(Guid id, Guid? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && u.Id != excludeId.Value);
        }

        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> UserExistsByUsernameAsync(string username, Guid? excludeUserId = null)
    {
        Ensure.ArgumentNotNullOrWhiteSpace(username, nameof(username));

        if (excludeUserId.HasValue)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Id != excludeUserId.Value);
        }

        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> UserExistsByEmailAsync(string email, Guid? excludeUserId = null)
    {
        Ensure.ArgumentNotNullOrWhiteSpace(email, nameof(email));

        if (excludeUserId.HasValue)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Id != excludeUserId.Value);
        }

        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}