namespace BookBase.Domain.Models.Entities;

public class BookCoverEntity
{
    public Guid Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public ICollection<BookEntity> Books { get; set; } = [];
}