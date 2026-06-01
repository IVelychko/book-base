using System.Diagnostics.CodeAnalysis;
using BookBase.Domain.Exceptions;

namespace BookBase.Domain.Shared;

public static class Ensure
{
    public static void ArgumentNotNull(object? argument) => ArgumentNullException.ThrowIfNull(argument);

    public static void ArgumentNotNullOrWhiteSpace(string? argument, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ValidationException(argumentName, "The value is null or whitespace");
        }
    }

    public static void EntityExists<T>([NotNull] T? entity, string? message = null) where T : class
    {
        if (entity is null)
        {
            throw new EntityNotFoundException(message);
        }
    }
}