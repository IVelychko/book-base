namespace BookBase.Domain.Models.Commands.Books;

public record AddBookCommand(
    string Title,
    string AuthorId,
    string PublisherId,
    string BookTypeId,
    string BookCoverId
);