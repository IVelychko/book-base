using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Models.Commands.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    [HttpGet]
    public async Task<IActionResult> GetAllBooksPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 12)
    {
        var result = await _bookService.GetAllBooksPaginatedAsync(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] Guid id)
    {
        var result = await _bookService.GetBookByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> AddBook([FromBody] AddBookRequest request)
    {
        var command = request.ToCommand();
        var result = await _bookService.AddBookAsync(command);
        return CreatedAtAction(nameof(GetBookById), new { id = result }, null);
    }

    [HttpPut]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
    {
        var command = request.ToCommand();
        await _bookService.UpdateBookAsync(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeleteBook([FromRoute] string id)
    {
        var command = new DeleteBookCommand(id);
        await _bookService.DeleteBookAsync(command);
        return NoContent();
    }
}