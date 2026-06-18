using BookBase.Application.Mapping.Extensions;
using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Models.Commands.Users;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;
using BookBase.Domain.Models.Results;
using BookBase.Domain.Shared;

namespace BookBase.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUserServiceValidator userServiceValidator) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    private readonly IUserServiceValidator _userServiceValidator = userServiceValidator;

    public async Task<Guid> AddUserAsync(AddUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _userServiceValidator.ValidateAddUserCommandAsync(command);

        var passwordHash = _passwordHasher.Hash(command.Password);
        var user = command.ToEntity(passwordHash);
        var userId = await _userRepository.AddUserAsync(user);
        return userId;
    }

    public async Task DeleteUserAsync(DeleteUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _userServiceValidator.ValidateDeleteUserCommandAsync(command);

        await _userRepository.DeleteUserAsync(Guid.Parse(command.Id));
    }

    public async Task<PagedResult<UserDto>> GetAllUsersPaginatedAsync(int pageNumber, int pageSize)
    {
        var userEntities = await _userRepository.GetAllUsersPaginatedAsync(pageNumber, pageSize);
        var totalUsersCount = await _userRepository.GetTotalUsersCountAsync();
        return new PagedResult<UserDto>
        {
            Items = userEntities.Select(u => u.ToDto()).ToList(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItemsCount = totalUsersCount
        };
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(id);
        Ensure.EntityExists(userEntity, "User with the specified ID does not exist.");
        return userEntity.ToDto();
    }
}