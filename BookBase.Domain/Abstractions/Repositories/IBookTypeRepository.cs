namespace BookBase.Domain.Abstractions.Repositories;

public interface IBookTypeRepository
{
    Task<bool> BookTypeExistsAsync(Guid id, Guid? excludeId = null);
}