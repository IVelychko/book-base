using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Models.Entities;

namespace BookBase.Application.Mapping.Extensions;

public static class PublisherMappingExtensions
{
    public static Publisher ToDto(this PublisherEntity entity)
    {
        return new Publisher
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}