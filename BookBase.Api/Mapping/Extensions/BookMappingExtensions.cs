using BookBase.Api.Models.Requests;
using BookBase.Domain.Models.Commands.Books;

namespace BookBase.Api.Mapping.Extensions;

public static class BookMappingExtensions
{
    public static AddBookCommand ToCommand(this AddBookRequest request)
    {
        return new AddBookCommand(
            Title: request.Title,
            AuthorId: request.AuthorId,
            PublisherId: request.PublisherId,
            BookTypeId: request.BookTypeId,
            BookCoverId: request.BookCoverId
        );
    }

    public static UpdateBookCommand ToCommand(this UpdateBookRequest request)
    {
        return new UpdateBookCommand(
            Id: request.Id,
            Title: request.Title,
            AuthorId: request.AuthorId,
            PublisherId: request.PublisherId,
            BookTypeId: request.BookTypeId,
            BookCoverId: request.BookCoverId
        );
    }
}