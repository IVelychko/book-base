namespace BookBase.Domain.Models.Entities;

public class GenreEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<BookGenreEntity> BookGenres { get; set; } = [];
}