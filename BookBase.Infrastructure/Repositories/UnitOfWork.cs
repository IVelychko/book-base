using BookBase.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookBase.Infrastructure.Repositories;

public class UnitOfWork(BooksDbContext context) : IUnitOfWork, IAsyncDisposable
{
    private readonly BooksDbContext _context = context;
    private IDbContextTransaction? _transaction;

    public async Task BeginChangesAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        if (_transaction is null)
        {
            return;
        }

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        GC.SuppressFinalize(this);
    }
}