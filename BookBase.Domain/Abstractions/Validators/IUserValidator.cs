using BookBase.Domain.Models.Commands.Users;
using FluentValidation.Results;

namespace BookBase.Domain.Abstractions.Validators;

public interface IUserValidator
{
    ValidationResult ValidateAddUserCommand(AddUserCommand command);

    ValidationResult ValidateDeleteUserCommand(DeleteUserCommand command);
}