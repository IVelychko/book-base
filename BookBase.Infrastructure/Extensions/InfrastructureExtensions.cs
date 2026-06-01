using BookBase.Domain.Abstractions.Repositories;
using BookBase.Domain.Exceptions;
using BookBase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBase.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddBooksDbContext(services, configuration);
        AddRepositories(services);

        return services;
    }

    private static IServiceCollection AddBooksDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringName = "BooksBaseConnection";
        var connectionString = configuration.GetConnectionString(connectionStringName) ??
            throw new ConfigurationException($"Connection string {connectionStringName} was not found");
        services.AddDbContext<BooksDbContext>(options =>
        {
            options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }

    private static IServiceCollection AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IBookTypeRepository, BookTypeRepository>();
        services.AddScoped<IBookCoverRepository, BookCoverRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}