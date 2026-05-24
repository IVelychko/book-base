using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBase.Infrastructure.EntityConfigurations;

public class BookCoverConfiguration : IEntityTypeConfiguration<BookCoverEntity>
{
    public void Configure(EntityTypeBuilder<BookCoverEntity> builder)
    {
        builder
            .HasIndex(bc => bc.Type)
            .IsUnique();
    }
}