using BookBase.Application.Services;
using BookBase.Application.Validation.Auth;
using BookBase.Application.Validation.Auth.Services;
using BookBase.Application.Validation.Books;
using BookBase.Application.Validation.Books.Services;
using BookBase.Application.Validation.Users;
using BookBase.Application.Validation.Users.Services;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Abstractions.Validators;
using BookBase.Domain.Abstractions.Validators.Services;
using BookBase.Domain.Models.Configurations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBase.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);

        AddJwtConfiguration(services, configuration);

        services.AddScoped<IBookValidator, BookValidator>();
        services.AddScoped<IUserValidator, UserValidator>();
        services.AddScoped<IAuthValidator, AuthValidator>();

        services.AddScoped<IBookServiceValidator, BookServiceValidator>();
        services.AddScoped<IUserServiceValidator, UserServiceValidator>();
        services.AddScoped<IAuthServiceValidator, AuthServiceValidator>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    private static IServiceCollection AddJwtConfiguration(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));

        return services;
    }
}