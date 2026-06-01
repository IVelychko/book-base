namespace BookBase.Domain.Abstractions.Validators.Services;

using BookBase.Domain.Models.Commands.Users;

public interface IUserServiceValidator
{
    Task ValidateAddUserCommandAsync(AddUserCommand command);

    Task ValidateDeleteUserCommandAsync(DeleteUserCommand command);
}