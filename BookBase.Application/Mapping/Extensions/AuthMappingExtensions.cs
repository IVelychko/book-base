using BookBase.Domain.Models.Commands.Auth;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class AuthMappingExtensions
{
    public static UserEntity ToEntity(this SignUpUserCommand command, string passwordHash, Guid roleId)
    {
        var userId = Guid.NewGuid();
        return new UserEntity
        {
            Id = userId,
            Username = command.Username,
            Email = command.Email,
            PasswordHash = passwordHash,
            UserRoles = [
                new UserRoleEntity
                {
                    UserId = userId,
                    RoleId = roleId
                }
            ]
        };
    }

    public static AuthorizedUserDto ToAuthorizedUserDto(this UserDto user, string accessToken)
    {
        return new AuthorizedUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            UserRoles = user.UserRoles,
            AccessToken = accessToken
        };
    }
}