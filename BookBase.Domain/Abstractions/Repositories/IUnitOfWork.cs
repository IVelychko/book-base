namespace BookBase.Domain.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task BeginChangesAsync();

    Task SaveChangesAsync();
}