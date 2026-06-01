using BookBase.Application.Extensions;
using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Users;
using BookBase.Domain.Shared;

namespace BookBase.Application.Validation.Users.Services;

public class UserServiceValidator(
    IUserValidator userValidator,
    IUserRepository userRepository,
    IRoleRepository roleRepository
) : IUserServiceValidator
{
    private readonly IUserValidator _userValidator = userValidator;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task ValidateAddUserCommandAsync(AddUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        var validationResult = _userValidator.ValidateAddUserCommand(command);
        validationResult.ThrowIfValidationFailed();

        var validationErrors = new Dictionary<string, string[]>();
        await CheckUserNotExistsByUsernameAsync(command.Username, validationErrors);
        await CheckUserNotExistsByEmailAsync(command.Email, validationErrors);
        foreach (var roleId in command.RoleIds)
        {
            await CheckRoleExistsAsync(Guid.Parse(roleId), validationErrors);
        }

        ThrowIfErrorsExist(validationErrors);
    }

    public async Task ValidateDeleteUserCommandAsync(DeleteUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        var validationResult = _userValidator.ValidateDeleteUserCommand(command);
        validationResult.ThrowIfValidationFailed();
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

    private async Task CheckUserNotExistsByUsernameAsync(string username, Dictionary<string, string[]> validationErrors, Guid? excludeBookId = null)
    {
        var exists = await _userRepository.UserExistsByUsernameAsync(username, excludeBookId);
        if (exists)
        {
            validationErrors.Add(nameof(username), ["User with the specified username already exists."]);
        }
    }

    private async Task CheckUserNotExistsByEmailAsync(string? email, Dictionary<string, string[]> validationErrors, Guid? excludeBookId = null)
    {
        if (email is null)
        {
            return;
        }

        var exists = await _userRepository.UserExistsByEmailAsync(email, excludeBookId);
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

    private static void ThrowIfErrorsExist(Dictionary<string, string[]> validationErrors)
    {
        if (validationErrors.Count > 0)
        {
            throw new ValidationException(validationErrors);
        }
    }
}