using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class UserRoleMappingExtensions
{
    public static UserRole ToDto(this UserRoleEntity entity)
    {
        return new UserRole
        {
            RoleId = entity.RoleId,
            Role = entity.Role?.Name ?? string.Empty
        };
    }
}