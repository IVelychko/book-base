namespace BookBase.Domain.Abstractions.Repositories;

public interface IPublisherRepository
{
    Task<bool> PublisherExistsAsync(Guid id, Guid? excludeId = null);
}