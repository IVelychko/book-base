namespace BookBase.Domain.Exceptions;

public class WrongPasswordException : AuthException
{
    public WrongPasswordException(string? message)
        : base(message)
    {
    }

    public WrongPasswordException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}