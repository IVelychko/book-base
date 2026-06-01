namespace BookBase.Domain.Exceptions;

public class RequiredRoleDoesNotExistException : Exception
{
    public RequiredRoleDoesNotExistException(string? message)
        : base(message)
    {
    }

    public RequiredRoleDoesNotExistException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}