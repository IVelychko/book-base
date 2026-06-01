using BookBase.Application.Extensions;
using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Auth;
using BookBase.Domain.Shared;

namespace BookBase.Application.Validation.Auth.Services;

public class AuthServiceValidator(
    IAuthValidator authValidator,
    IUserRepository userRepository
) : IAuthServiceValidator
{
    private readonly IAuthValidator _authValidator = authValidator;

    private readonly IUserRepository _userRepository = userRepository;

    public async Task ValidateSignInUserCommandAsync(SignInUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        var validationResult = _authValidator.ValidateSignInUserCommand(command);
        validationResult.ThrowIfValidationFailed();

        var validationErrors = new Dictionary<string, string[]>();
        await CheckSignInUserExistsByUsernameAsync(command.Username);

        ThrowIfErrorsExist(validationErrors);
    }

    public async Task ValidateSignUpUserCommandAsync(SignUpUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        var validationResult = _authValidator.ValidateSignUpUserCommand(command);
        validationResult.ThrowIfValidationFailed();

        var validationErrors = new Dictionary<string, string[]>();
        await CheckUserNotExistsByUsernameAsync(command.Username, validationErrors);
        await CheckUserNotExistsByEmailAsync(command.Email, validationErrors);

        ThrowIfErrorsExist(validationErrors);
    }

    private async Task CheckSignInUserExistsByUsernameAsync(string username)
    {
        var exists = await _userRepository.UserExistsByUsernameAsync(username);
        if (!exists)
        {
            throw new SignInUserNotFoundException("Wrong credentials.");
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

    private static void ThrowIfErrorsExist(Dictionary<string, string[]> validationErrors)
    {
        if (validationErrors.Count > 0)
        {
            throw new ValidationException(validationErrors);
        }
    }
}