using System.Text;
using BookBase.Domain.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BookBase.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, string policyName, IConfiguration configuration)
    {
        string[] allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtConfiguration jwtConfiguration = new();
        configuration.GetSection(nameof(JwtConfiguration)).Bind(jwtConfiguration);
        var encodedKey = Encoding.UTF8.GetBytes(jwtConfiguration.Key);
        SymmetricSecurityKey symmetricSecurityKey = new(encodedKey);
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfiguration.Issuer,
            ValidAudience = jwtConfiguration.Audience,
            IssuerSigningKey = symmetricSecurityKey,
            ClockSkew = TimeSpan.Zero,
        };

        services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = tokenValidationParameters;
        });

        return services;
    }
}