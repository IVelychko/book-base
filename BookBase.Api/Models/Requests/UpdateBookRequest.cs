namespace BookBase.Api.Models.Requests;

public record UpdateBookRequest(
    string Id,
    string Title,
    string AuthorId,
    string PublisherId,
    string BookTypeId,
    string BookCoverId
);