using BookBase.Application.Services;
using BookBase.Application.Validation.Books;
using BookBase.Application.Validation.Books.Services;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Abstractions.Validators.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookBase.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);

        services.AddScoped<IBookValidator, BookValidator>();

        services.AddScoped<IBookServiceValidator, BookServiceValidator>();

        services.AddScoped<IBookService, BookService>();

        return services;
    }
}