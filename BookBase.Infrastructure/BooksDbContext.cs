using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Infrastructure;

public class BooksDbContext(DbContextOptions<BooksDbContext> options) : DbContext(options)
{
    public DbSet<BookEntity> Books => Set<BookEntity>();

    public DbSet<BookTypeEntity> BookTypes => Set<BookTypeEntity>();

    public DbSet<BookCoverEntity> BookCovers => Set<BookCoverEntity>();

    public DbSet<GenreEntity> Genres => Set<GenreEntity>();

    public DbSet<PublisherEntity> Publishers => Set<PublisherEntity>();

    public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();

    public DbSet<BookGenreEntity> BookGenres => Set<BookGenreEntity>();

    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<RoleEntity> Roles => Set<RoleEntity>();

    public DbSet<UserRoleEntity> UserRoles => Set<UserRoleEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("bookbase");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);
    }
}