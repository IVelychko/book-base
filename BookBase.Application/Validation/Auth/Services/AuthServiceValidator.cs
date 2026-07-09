using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Auth;

namespace BookBase.Application.Validation.Auth.Services;

public class AuthServiceValidator(
    IUserRepository userRepository
) : IAuthServiceValidator
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task ValidateSignInUserCommandAsync(SignInUserCommand command)
    {
        await CheckSignInUserExistsByUsernameAsync(command.Username);
    }

    public async Task ValidateSignUpUserCommandAsync(SignUpUserCommand command)
    {
        var validationErrors = new Dictionary<string, string[]>();
        await CheckUserNotExistsByUsernameAsync(command.Username, validationErrors);
        await CheckUserNotExistsByEmailAsync(command.Email, validationErrors);

        ValidationHelper.ThrowIfErrorsExist(validationErrors);
    }

    private async Task CheckSignInUserExistsByUsernameAsync(string username)
    {
        var exists = await _userRepository.UserExistsByUsernameAsync(username);
        if (!exists)
        {
            throw new SignInUserNotFoundException("Wrong credentials.");
        }
    }

    private async Task CheckUserNotExistsByUsernameAsync(string username, Dictionary<string, string[]> validationErrors, Guid? excludeAuthId = null)
    {
        var exists = await _userRepository.UserExistsByUsernameAsync(username, excludeAuthId);
        if (exists)
        {
            validationErrors.Add(nameof(username), ["User with the specified username already exists."]);
        }
    }

    private async Task CheckUserNotExistsByEmailAsync(string? email, Dictionary<string, string[]> validationErrors, Guid? excludeAuthId = null)
    {
        if (email is null)
        {
            return;
        }

        var exists = await _userRepository.UserExistsByEmailAsync(email, excludeAuthId);
        if (exists)
        {
            validationErrors.Add(nameof(email), ["User with the specified email already exists."]);
        }
    }
}