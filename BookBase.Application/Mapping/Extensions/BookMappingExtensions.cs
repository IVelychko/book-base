using BookBase.Domain.Models.Commands.Books;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class BookMappingExtensions
{
    public static BookEntity ToEntity(this AddBookCommand command)
    {
        return new BookEntity
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            AuthorId = Guid.Parse(command.AuthorId),
            PublisherId = Guid.Parse(command.PublisherId),
            BookTypeId = Guid.Parse(command.BookTypeId),
            BookCoverId = Guid.Parse(command.BookCoverId),
            PublicationDate = DateTime.UtcNow
        };
    }

    public static BookEntity ToEntity(this UpdateBookCommand command, DateTime publicationDate)
    {
        return new BookEntity
        {
            Id = Guid.Parse(command.Id),
            Title = command.Title,
            AuthorId = Guid.Parse(command.AuthorId),
            PublisherId = Guid.Parse(command.PublisherId),
            BookTypeId = Guid.Parse(command.BookTypeId),
            BookCoverId = Guid.Parse(command.BookCoverId),
            PublicationDate = publicationDate
        };
    }

    public static Book ToDto(this BookEntity entity)
    {
        return new Book
        {
            Id = entity.Id,
            Title = entity.Title,
            PublicationDate = entity.PublicationDate,
            AuthorId = entity.AuthorId,
            Author = entity.Author?.ToDto(),
            PublisherId = entity.PublisherId,
            Publisher = entity.Publisher?.ToDto(),
            BookTypeId = entity.BookTypeId,
            BookType = entity.BookType?.Name ?? string.Empty,
            BookCoverId = entity.BookCoverId,
            BookCover = entity.BookCover?.Type ?? string.Empty
        };
    }
}