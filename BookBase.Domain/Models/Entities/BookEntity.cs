namespace BookBase.Domain.Models.Entities;

public class BookEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime PublicationDate { get; set; }

    public Guid AuthorId { get; set; }

    public Guid PublisherId { get; set; }

    public Guid BookTypeId { get; set; }

    public Guid BookCoverId { get; set; }

    public AuthorEntity? Author { get; set; }

    public PublisherEntity? Publisher { get; set; }

    public BookTypeEntity? BookType { get; set; }

    public BookCoverEntity? BookCover { get; set; }

    public ICollection<BookGenreEntity> BookGenres { get; set; } = [];
}