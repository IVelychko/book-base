using BookBase.Domain.Models.Commands.Users;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class UserMappingExtensions
{
    public static UserEntity ToEntity(this AddUserCommand command, string passwordHash)
    {
        var userId = Guid.NewGuid();
        return new UserEntity
        {
            Id = userId,
            Username = command.Username,
            Email = command.Email,
            PasswordHash = passwordHash,
            UserRoles = CreateUserRoles(command.RoleIds, userId)
        };
    }

    public static User ToDto(this UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            UserRoles = entity.UserRoles.Select(ur => ur.ToDto()).ToList()
        };
    }

    private static List<UserRoleEntity> CreateUserRoles(IEnumerable<string> roleIds, Guid userId)
    {
        return roleIds.Select(roleId => new UserRoleEntity
        {
            UserId = userId,
            RoleId = Guid.Parse(roleId)
        })
        .ToList();
    }
}