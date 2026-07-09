using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Models.DTOs;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Books;

public class GetBookByIdEndpoint(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
    public override void Configure()
    {
        Get("/api/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var result = await bookService.GetBookByIdAsync(req.Id);
        await SendOkAsync(result, ct);
    }
}