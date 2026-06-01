using BookBase.Domain.Models.Entities;

namespace BookBase.Domain.Abstractions.Repositories;

public interface IRoleRepository
{
    Task<RoleEntity?> GetRoleByNameAsync(string name);

    Task<bool> RoleExistsAsync(Guid id, Guid? excludeId = null);
}