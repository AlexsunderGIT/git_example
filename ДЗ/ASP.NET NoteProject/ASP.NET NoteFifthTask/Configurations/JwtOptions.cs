using System.ComponentModel.DataAnnotations;

public class JwtOptions
{
    // Название приложения, которое будет подписывать токен.
    [Required]
    public required string Issuer { get; init; }
    // Название приложения, для которого подписываем токен.
    [Required]
    public required string Audience { get; init; }
    // Секретная фраза для шифрования.
    [Required]
    public required string Secret { get; init; }
}