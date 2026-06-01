namespace BookBase.Domain.Exceptions;

public class SignInUserNotFoundException : AuthException
{
    public SignInUserNotFoundException(string? message)
        : base(message)
    {
    }

    public SignInUserNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}