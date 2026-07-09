using BookBase.Domain.Exceptions;

namespace BookBase.Application.Validation;

public static class ValidationHelper
{
    public static void ThrowIfErrorsExist(Dictionary<string, string[]> validationErrors)
    {
        if (validationErrors.Count > 0)
        {
            throw new ValidationException(validationErrors);
        }
    }
}