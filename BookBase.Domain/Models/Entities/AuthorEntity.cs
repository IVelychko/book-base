namespace BookBase.Domain.Models.Entities;

public class AuthorEntity
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Pseudonym { get; set; }

    public ICollection<BookEntity> Books { get; set; } = [];
}