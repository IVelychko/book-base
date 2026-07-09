using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using BookBase.Domain.Models.Commands.Books;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Books;

public class DeleteBookEndpoint(IBookService bookService) : Endpoint<DeleteBookRequest>
{
    public override void Configure()
    {
        Delete("/api/books/{Id}");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        var command = new DeleteBookCommand(req.Id);
        await bookService.DeleteBookAsync(command);
        await SendNoContentAsync(ct);
    }
}