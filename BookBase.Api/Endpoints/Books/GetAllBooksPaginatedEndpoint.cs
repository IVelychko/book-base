using BookBase.Api.Models.Requests;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Results;
using FastEndpoints;

namespace BookBase.Api.Endpoints.Books;

public class GetAllBooksPaginatedEndpoint(IBookService bookService)
    : Endpoint<GetAllBooksPaginatedRequest, PagedResult<BookDto>>
{
    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllBooksPaginatedRequest req, CancellationToken ct)
    {
        var result = await bookService.GetAllBooksPaginatedAsync(req.PageNumber, req.PageSize);
        await SendOkAsync(result, ct);
    }
}