using System.Security.Cryptography;
using System.Text;

namespace AuthExample.Utils;
// Статический класс, так как нам не нужен экземпляр отдельный для шифрования.
public static class PasswordHasher
{
    // Лучше указывать от 100к.
    private const int HashingIterations = 100000;
    private const int HashSize = 32; // 256 bits
    private const int SaltSize = 16; // 128 bits
                                     // Алгоритм, который мы используем.
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    public static byte[] HashPassword(string password)
    {
        // Генерируем случайное число в качестве примеси к шифрованному паролю.
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        // Генерируем пароль с примисью.
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashingIterations,
        _hashAlgorithm, HashSize);
        // Возвращаем специальную строку.
        // Переводим в byte формат, чтобы еще сложнее было определить, что за пароль, так как нужно будет еще кодировку угадать.
        return Encoding.UTF8.GetBytes($"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}");
    }
    public static bool VerifyPassword(byte[] hashedPassword, string password)
    {
        var hashedPasswordString = Encoding.UTF8.GetString(hashedPassword);
        string[] parts = hashedPasswordString.Split('-');
        if (parts.Length != 2)
            throw new FormatException("Invalid hashed password format.");
        byte[] salt = Convert.FromHexString(parts[0]);
        byte[] hash = Convert.FromHexString(parts[1]);
        byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashingIterations,
        _hashAlgorithm, HashSize);
        // Позволяет избежать проблем, когда время сравнения хэшей используется, чтобы определить его длину.
        return CryptographicOperations.FixedTimeEquals(hash, computedHash);
    }
}