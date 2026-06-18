using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookBase.Domain.Abstractions.Services;
using BookBase.Domain.Models.Configurations;
using BookBase.Domain.Models.DTOs;
using BookBase.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookBase.Application.Services;

public class JwtService(IOptions<JwtConfiguration> jwtConfiguration) : IJwtService
{
    private readonly JwtConfiguration _jwtConfiguration = jwtConfiguration.Value;

    public JwtSecurityToken ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
    {
        Ensure.ArgumentNotNullOrWhiteSpace(token, nameof(token));
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
        return (JwtSecurityToken)validatedToken;
    }

    public string CreateSerializedToken(UserDto user)
    {
        Ensure.ArgumentNotNull(user);
        var token = CreateToken(user);
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateToken(UserDto user)
    {
        var claims = GetClaims(user);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = GetExpirationTime(),
            Issuer = _jwtConfiguration.Issuer,
            Audience = _jwtConfiguration.Audience,
            SigningCredentials = GetSigningCredentials(),
        };
        JwtSecurityTokenHandler tokenHandler = new();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (JwtSecurityToken)token;
    }

    private DateTime GetExpirationTime()
    {
        var tokenLifeTime = _jwtConfiguration.Lifetime.Split(':');
        var hours = int.Parse(tokenLifeTime[0]);
        var minutes = int.Parse(tokenLifeTime[1]);
        var seconds = int.Parse(tokenLifeTime[2]);
        return DateTime.UtcNow
            .AddHours(hours)
            .AddMinutes(minutes)
            .AddSeconds(seconds);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var encodedKey = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
        SymmetricSecurityKey securityKey = new(encodedKey);
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(UserDto user)
    {
        var userRoleClaims = user.UserRoles.Select(x => new Claim(ClaimTypes.Role, x.Role)).ToList();
        List<Claim> claims = [
            .. userRoleClaims,
            new(JwtRegisteredClaimNames.Sub, _jwtConfiguration.Subject),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
            new("id", user.Id.ToString())
        ];
        return claims;
    }
}