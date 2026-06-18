using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class AuthorMappingExtensions
{
    public static AuthorDto ToDto(this AuthorEntity entity)
    {
        return new AuthorDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Pseudonym = entity.Pseudonym
        };
    }
}