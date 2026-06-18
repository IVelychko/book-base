using System.IdentityModel.Tokens.Jwt;
using BookBase.Domain.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace BookBase.Domain.Abstractions.Services;

public interface IJwtService
{
    string CreateSerializedToken(UserDto user);

    JwtSecurityToken ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
}