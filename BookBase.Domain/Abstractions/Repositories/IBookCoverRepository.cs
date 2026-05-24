namespace BookBase.Domain.Abstractions.Repositories;

public interface IBookCoverRepository
{
    Task<bool> BookCoverExistsAsync(Guid id, Guid? excludeId = null);
}