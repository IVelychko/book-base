using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Users;

namespace BookBase.Application.Validation.Users.Services;

public class UserServiceValidator(
    IUserRepository userRepository,
    IRoleRepository roleRepository
) : IUserServiceValidator
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task ValidateAddUserCommandAsync(AddUserCommand command)
    {
        var validationErrors = new Dictionary<string, string[]>();
        await CheckUserNotExistsByUsernameAsync(command.Username, validationErrors);
        await CheckUserNotExistsByEmailAsync(command.Email, validationErrors);
        foreach (var roleId in command.RoleIds)
        {
            await CheckRoleExistsAsync(Guid.Parse(roleId), validationErrors);
        }

        ValidationHelper.ThrowIfErrorsExist(validationErrors);
    }

    public async Task ValidateDeleteUserCommandAsync(DeleteUserCommand command)
    {
        await CheckUserExistsByIdAsync(Guid.Parse(command.Id));
    }

    private async Task CheckUserExistsByIdAsync(Guid userId)
    {
        var exists = await _userRepository.UserExistsAsync(userId);
        if (!exists)
        {
            throw new EntityNotFoundException("User with the specified ID does not exist.");
        }
    }

    private async Task CheckUserNotExistsByUsernameAsync(string username, Dictionary<string, string[]> validationErrors, Guid? excludeUserId = null)
    {
        var exists = await _userRepository.UserExistsByUsernameAsync(username, excludeUserId);
        if (exists)
        {
            validationErrors.Add(nameof(username), ["User with the specified username already exists."]);
        }
    }

    private async Task CheckUserNotExistsByEmailAsync(string? email, Dictionary<string, string[]> validationErrors, Guid? excludeUserId = null)
    {
        if (email is null)
        {
            return;
        }

        var exists = await _userRepository.UserExistsByEmailAsync(email, excludeUserId);
        if (exists)
        {
            validationErrors.Add(nameof(email), ["User with the specified email already exists."]);
        }
    }

    private async Task CheckRoleExistsAsync(Guid roleId, Dictionary<string, string[]> validationErrors)
    {
        var exists = await _roleRepository.RoleExistsAsync(roleId);
        if (!exists)
        {
            const string roleIdsKey = "roleIds";
            validationErrors.TryGetValue(roleIdsKey, out var existingErrors);
            if (existingErrors is not null)
            {
                string[] updatedErrors = [.. existingErrors, $"Role with ID {roleId} does not exist."];
                validationErrors[roleIdsKey] = updatedErrors;
                return;
            }

            validationErrors.Add(roleIdsKey, [$"Role with ID {roleId} does not exist."]);
        }
    }
}