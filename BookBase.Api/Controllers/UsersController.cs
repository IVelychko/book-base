using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Models.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetAllUsersPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 12)
    {
        var result = await _userService.GetAllUsersPaginatedAsync(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
    {
        var command = request.ToCommand();
        var result = await _userService.AddUserAsync(command);
        return CreatedAtAction(nameof(GetUserById), new { id = result }, null);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        var command = new DeleteUserCommand(id);
        await _userService.DeleteUserAsync(command);
        return NoContent();
    }
}