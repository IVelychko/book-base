using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInUser(SignInUserRequest request)
    {
        var command = request.ToCommand();
        var result = await _authService.SignInUserAsync(command);
        return Ok(result);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpUser(SignUpUserRequest request)
    {
        var command = request.ToCommand();
        var result = await _authService.SignUpUserAsync(command);
        return Ok(result);
    }
}