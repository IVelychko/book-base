namespace BookBase.Domain.Models.Configurations;

public class JwtConfiguration
{
    public string Audience { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string Lifetime { get; set; } = string.Empty;
}