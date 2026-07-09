using BookBase.Api.Mapping.Extensions;
using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Constants;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Books;

public class UpdateBookEndpoint(IBookService bookService) : Endpoint<UpdateBookRequest>
{
    public override void Configure()
    {
        Put("/api/books");
        Roles(RoleNames.Admin);
    }

    public override async Task HandleAsync(UpdateBookRequest req, CancellationToken ct)
    {
        var command = req.ToCommand();
        await bookService.UpdateBookAsync(command);
        await SendNoContentAsync(ct);
    }
}