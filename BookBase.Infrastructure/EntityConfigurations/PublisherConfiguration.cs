using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBase.Infrastructure.EntityConfigurations;

public class PublisherConfiguration : IEntityTypeConfiguration<PublisherEntity>
{
    public void Configure(EntityTypeBuilder<PublisherEntity> builder)
    {
        builder
            .HasIndex(p => p.Name)
            .IsUnique();
    }
}