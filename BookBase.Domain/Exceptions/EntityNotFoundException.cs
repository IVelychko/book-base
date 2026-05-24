namespace BookBase.Domain.Exceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string? message)
        : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}