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
}