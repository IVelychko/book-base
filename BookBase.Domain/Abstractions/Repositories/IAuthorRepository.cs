namespace BookBase.Domain.Abstractions.Repositories;

public interface IAuthorRepository
{
    Task<bool> AuthorExistsAsync(Guid id, Guid? excludeId = null);
}