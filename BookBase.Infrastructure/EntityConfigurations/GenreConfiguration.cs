using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBase.Infrastructure.EntityConfigurations;

public class GenreConfiguration : IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        builder
            .HasIndex(g => g.Name)
            .IsUnique();
    }
}