namespace BookBase.Domain.Models.DTOs;

public class Author
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Pseudonym { get; set; }
}