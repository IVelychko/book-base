using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBase.Infrastructure.EntityConfigurations;

public class BookTypeConfiguration : IEntityTypeConfiguration<BookTypeEntity>
{
    public void Configure(EntityTypeBuilder<BookTypeEntity> builder)
    {
        builder
            .HasIndex(bt => bt.Name)
            .IsUnique();
    }
}