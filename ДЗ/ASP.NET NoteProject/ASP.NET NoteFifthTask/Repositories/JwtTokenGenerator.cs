﻿using ConsoleProject.NET.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConsoleProject.NET.Repositories;

public class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
{
    private readonly JwtOptions _options = options.Value;
    public JwtToken Generate(User user)
    {
        // Настройки подписи токена.
        SigningCredentials credentials = new(
        // Расшифровываем секрет из настроек.
        new SymmetricSecurityKey(Convert.FromBase64String(_options.Secret)),
        SecurityAlgorithms.HmacSha256
        );
        // Клеймы (метаданные пользователя, которому выдаем токен).
        var claims = new[]
        {
         new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
         new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
         new Claim(JwtRegisteredClaimNames.FamilyName, user.Name),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var now = DateTime.UtcNow;
        // Время жизни. По очевидным причинам не стоит иметь слишком большим.
        var expiration = now.AddMinutes(5);
        JwtSecurityToken securityToken = new(
        // Сервис, который подписал токен.
        issuer: _options.Issuer,
        // Сервис, для которого подписан токен.
        audience: _options.Audience,
        expires: expiration,
        claims: claims,
        signingCredentials: credentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return new JwtToken
        {
            UserId = user.Id,
            Token = token,
            CreatedAt = now,
            ExpiresAt = expiration,
        };
    }
}
