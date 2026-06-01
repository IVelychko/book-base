using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Models.Commands.Users;
using BookBase.Domain.Shared;
using FluentValidation;
using FluentValidation.Results;

namespace BookBase.Application.Validation.Users;

public class UserValidator(
    IValidator<AddUserCommand> addUserCommandValidator,
    IValidator<DeleteUserCommand> deleteUserCommandValidator

) : IUserValidator
{
    public ValidationResult ValidateAddUserCommand(AddUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return addUserCommandValidator.Validate(command);
    }

    public ValidationResult ValidateDeleteUserCommand(DeleteUserCommand command)
    {
        Ensure.ArgumentNotNull(command);
        return deleteUserCommandValidator.Validate(command);
    }
}