namespace BookBase.Domain.Models.Entities;

public class BookGenreEntity
{
    public Guid BookId { get; set; }

    public BookEntity? Book { get; set; }

    public Guid GenreId { get; set; }

    public GenreEntity? Genre { get; set; }
}