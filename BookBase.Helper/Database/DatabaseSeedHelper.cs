using Bogus;
using BookBase.Domain.Models.Entities;
using BookBase.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookBase.Helper.Database;

public static class SeedDatabase
{
    public static async Task SeedAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__BooksBaseConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is not set in environment variables.");
        }

        var options = new DbContextOptionsBuilder<BooksDbContext>()
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
            .Options;

        using BooksDbContext context = new(options);

        var authorFaker = new Faker<AuthorEntity>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.FirstName, f => f.Person.FirstName)
            .RuleFor(a => a.LastName, f => f.Person.LastName)
            .RuleFor(a => a.Pseudonym, f => f.Company.CompanyName());

        AuthorEntity[] authors = authorFaker.Generate(15).ToArray();
        if (!context.Authors.Any())
        {
            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        var publisherFaker = new Faker<PublisherEntity>()
            .RuleFor(p => p.Id, _ => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Company.CompanyName());

        PublisherEntity[] publishers = publisherFaker.Generate(10).ToArray();
        if (!context.Publishers.Any())
        {
            await context.Publishers.AddRangeAsync(publishers);
            await context.SaveChangesAsync();
        }

        BookTypeEntity[] bookTypes =
        [
            new BookTypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Manga"
            },
            new BookTypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Book"
            },
            new BookTypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Comic"
            }
        ];
        if (!context.BookTypes.Any())
        {
            await context.BookTypes.AddRangeAsync(bookTypes);
            await context.SaveChangesAsync();
        }

        BookCoverEntity[] bookCovers =
        [
            new BookCoverEntity
            {
                Id = Guid.NewGuid(),
                Type = "Paperback"
            },
            new BookCoverEntity
            {
                Id = Guid.NewGuid(),
                Type = "Hardcover"
            },
            new BookCoverEntity
            {
                Id = Guid.NewGuid(),
                Type = "Digital"
            }
        ];
        if (!context.BookCovers.Any())
        {
            await context.BookCovers.AddRangeAsync(bookCovers);
            await context.SaveChangesAsync();
        }

        GenreEntity[] genres =
        [
            new GenreEntity
            {
                Id = Guid.NewGuid(),
                Name = "Action"
            },
            new GenreEntity
            {
                Id = Guid.NewGuid(),
                Name = "Adventure"
            },
            new GenreEntity
            {
                Id = Guid.NewGuid(),
                Name = "Fantasy"
            },
            new GenreEntity
            {
                Id = Guid.NewGuid(),
                Name = "Horror"
            },
            new GenreEntity
            {
                Id = Guid.NewGuid(),
                Name = "Science Fiction"
            }
        ];
        if (!context.Genres.Any())
        {
            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();
        }

        var bookFaker = new Faker<BookEntity>()
            .RuleFor(b => b.Id, _ => Guid.NewGuid())
            .RuleFor(b => b.Title, f => f.Name.FullName() + ": " + f.Random.Words(2))
            .RuleFor(b => b.AuthorId, (f, _) => authors[f.Random.Int(0, authors.Length - 1)].Id)
            .RuleFor(b => b.PublisherId, (f, _) => publishers[f.Random.Int(0, publishers.Length - 1)].Id)
            .RuleFor(b => b.BookTypeId, (f, _) => bookTypes[f.Random.Int(0, bookTypes.Length - 1)].Id)
            .RuleFor(b => b.BookCoverId, (f, _) => bookCovers[f.Random.Int(0, bookCovers.Length - 1)].Id)
            .RuleFor(b => b.PublicationDate, f => f.Date.PastDateOnly(yearsToGoBack: 50).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));

        BookEntity[] books = bookFaker.Generate(20).ToArray();
        if (!context.Books.Any())
        {
            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        var bookGenres = new List<BookGenreEntity>();
        var bookGenreFaker = new Faker();
        foreach (var book in books)
        {
            var genreCount = bookGenreFaker.Random.Int(1, 2);
            var randomGenres = bookGenreFaker.Random.ListItems(genres.ToList(), genreCount);

            foreach (var genre in randomGenres)
            {
                bookGenres.Add(new BookGenreEntity { BookId = book.Id, GenreId = genre.Id });
            }
        }

        if (!context.BookGenres.Any())
        {
            await context.BookGenres.AddRangeAsync(bookGenres);
            await context.SaveChangesAsync();
        }
    }
}