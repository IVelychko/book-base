using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class UserRoleMappingExtensions
{
    public static UserRoleDto ToDto(this UserRoleEntity entity)
    {
        return new UserRoleDto
        {
            RoleId = entity.RoleId,
            Role = entity.Role?.Name ?? string.Empty
        };
    }
}