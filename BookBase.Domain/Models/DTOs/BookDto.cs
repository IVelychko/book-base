namespace BookBase.Domain.Models.DTOs;

public class BookDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }

    public Guid AuthorId { get; set; }

    public AuthorDto? Author { get; set; }

    public Guid PublisherId { get; set; }

    public PublisherDto? Publisher { get; set; }

    public Guid BookTypeId { get; set; }

    public string BookType { get; set; } = string.Empty;

    public Guid BookCoverId { get; set; }

    public string BookCover { get; set; } = string.Empty;
}