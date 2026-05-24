using BookBase.Domain.Exceptions;
using FluentValidation.Results;

namespace BookBase.Application.Extensions;

public static class ValidationResultExtensions
{
    public static void ThrowIfValidationFailed(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
        {
            return;
        }

        throw new ValidationException(validationResult.ToDictionary());
    }
}