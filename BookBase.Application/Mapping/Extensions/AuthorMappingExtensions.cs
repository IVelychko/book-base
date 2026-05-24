using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class AuthorMappingExtensions
{
    public static Author ToDto(this AuthorEntity entity)
    {
        return new Author
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Pseudonym = entity.Pseudonym
        };
    }
}