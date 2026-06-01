namespace BookBase.Domain.Exceptions;

public class DbException : Exception
{
    public DbException(string? message)
        : base(message)
    {
    }

    public DbException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}