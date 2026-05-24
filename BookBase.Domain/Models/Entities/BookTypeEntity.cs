namespace BookBase.Domain.Models.Entities;

public class BookTypeEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<BookEntity> Books { get; set; } = [];
}