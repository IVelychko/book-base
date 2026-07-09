using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Books;

public class AddBookEndpoint(IBookService bookService) : Endpoint<AddBookRequest>
{
    public override void Configure()
    {
        Post("/api/books");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(AddBookRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        var result = await bookService.AddBookAsync(command);
        await SendCreatedAtAsync<GetBookByIdEndpoint>(new { Id = result }, null, cancellation: ct);
    }
}