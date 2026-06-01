using BookBase.Application.Mapping.Extensions;
using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Exceptions;
using BookBase.Domain.Models.Commands.Auth;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;
using BookBase.Domain.Shared;

namespace BookBase.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService,
    IAuthServiceValidator authServiceValidator
) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IRoleRepository _roleRepository = roleRepository;

    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    private readonly IJwtService _jwtService = jwtService;

    private readonly IAuthServiceValidator _authServiceValidator = authServiceValidator;

    public async Task<AuthorizedUser> SignInUserAsync(SignInUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _authServiceValidator.ValidateSignInUserCommandAsync(command);

        var userEntity = await _userRepository.GetUserByUsernameAsync(command.Username);
        Ensure.EntityExists(userEntity, "User with the specified username does not exist.");
        VerifyPassword(command, userEntity);
        return CreateAuthorizedUser(userEntity);
    }

    public async Task<AuthorizedUser> SignUpUserAsync(SignUpUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        await _authServiceValidator.ValidateSignUpUserCommandAsync(command);

        var userEntity = await CreateUserEntityAsync(command);
        var userId = await _userRepository.AddUserAsync(userEntity);
        var addedUserEntity = await _userRepository.GetUserByIdAsync(userId)
            ?? throw new DbException($"User with ID {userId} is not present in the database after the insertion");
        return CreateAuthorizedUser(addedUserEntity);
    }

    private void VerifyPassword(SignInUserCommand command, UserEntity userEntity)
    {
        if (_passwordHasher.Verify(command.Password, userEntity.PasswordHash))
        {
            return;
        }

        throw new WrongPasswordException("Wrong credentials.");
    }

    private async Task<UserEntity> CreateUserEntityAsync(SignUpUserCommand command)
    {
        var passwordHash = _passwordHasher.Hash(command.Password);
        var roleEntity = await GetRoleEntityByNameAsync(RoleNames.User);
        return command.ToEntity(passwordHash, roleEntity.Id);
    }

    private async Task<RoleEntity> GetRoleEntityByNameAsync(string name)
    {
        var roleEntity = await _roleRepository.GetRoleByNameAsync(name)
            ?? throw new RequiredRoleDoesNotExistException($"Role {name} does not exist in the database");
        return roleEntity;
    }

    private AuthorizedUser CreateAuthorizedUser(UserEntity userEntity)
    {
        var user = userEntity.ToDto();
        var accessToken = _jwtService.CreateSerializedToken(user);
        var authorizedUser = user.ToAuthorizedUserDto(accessToken);
        return authorizedUser;
    }
}