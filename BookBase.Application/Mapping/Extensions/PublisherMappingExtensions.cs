using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class PublisherMappingExtensions
{
    public static PublisherDto ToDto(this PublisherEntity entity)
    {
        return new PublisherDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}