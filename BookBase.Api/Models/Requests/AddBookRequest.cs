namespace BookBase.Api.Models.Requests;

public record AddBookRequest(
    string Title,
    string AuthorId,
    string PublisherId,
    string BookTypeId,
    string BookCoverId
);