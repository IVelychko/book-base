namespace BookBase.Domain.Models.DTOs;

public class Book
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }

    public Guid AuthorId { get; set; }

    public Author? Author { get; set; }

    public Guid PublisherId { get; set; }

    public Publisher? Publisher { get; set; }

    public Guid BookTypeId { get; set; }

    public string BookType { get; set; } = string.Empty;

    public Guid BookCoverId { get; set; }

    public string BookCover { get; set; } = string.Empty;
}