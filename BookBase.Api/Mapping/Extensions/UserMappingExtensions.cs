using BookBase.Api.Models.Requests;
using BookBase.Domain.Models.Commands.Users;

namespace BookBase.Api.Mapping.Extensions;

public static class UserMappingExtensions
{
    public static AddUserCommand ToCommand(this AddUserRequest request)
    {
        return new AddUserCommand(
            request.Username,
            request.Email,
            request.Password,
            request.RoleIds
        );
    }
}