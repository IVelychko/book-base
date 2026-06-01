using BookBase.Domain.Models.Entities;

namespace BookBase.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<IList<UserEntity>> GetAllUsersPaginatedAsync(int pageNumber, int pageSize);

    Task<UserEntity?> GetUserByIdAsync(Guid id);

    Task<UserEntity?> GetUserByUsernameAsync(string username);

    Task<Guid> AddUserAsync(UserEntity user);

    Task UpdateUserAsync(UserEntity user);

    Task DeleteUserAsync(Guid id);

    Task<bool> UserExistsAsync(Guid id, Guid? excludeId = null);

    Task<bool> UserExistsByUsernameAsync(string username, Guid? excludeUserId = null);

    Task<bool> UserExistsByEmailAsync(string email, Guid? excludeUserId = null);

    Task<int> GetTotalUsersCountAsync();
}