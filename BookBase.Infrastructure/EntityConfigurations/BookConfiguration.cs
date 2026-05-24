using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBase.Infrastructure.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder
            .HasIndex(b => new { b.Title, b.AuthorId })
            .IsUnique();

        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(b => b.BookType)
            .WithMany(bt => bt.Books)
            .HasForeignKey(b => b.BookTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.BookCover)
            .WithMany(bc => bc.Books)
            .HasForeignKey(b => b.BookCoverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}