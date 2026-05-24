namespace BookBase.Domain.Models.Commands.Books;

public record UpdateBookCommand(
    string Id,
    string Title,
    string AuthorId,
    string PublisherId,
    string BookTypeId,
    string BookCoverId
);