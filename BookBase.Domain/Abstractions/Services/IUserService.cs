using BookBase.Domain.Models.Commands.Users;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Results;

namespace BookBase.Domain.Abstractions.Services;

public interface IUserService
{
    Task<PagedResult<User>> GetAllUsersPaginatedAsync(int pageNumber, int pageSize);

    Task<User> GetUserByIdAsync(Guid id);

    Task<Guid> AddUserAsync(AddUserCommand command);

    Task DeleteUserAsync(DeleteUserCommand command);
}