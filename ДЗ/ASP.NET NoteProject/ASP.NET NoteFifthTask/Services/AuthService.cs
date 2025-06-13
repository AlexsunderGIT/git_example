using AuthExample.Utils;
using ConsoleProject.NET.Abstractions;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Database;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Configurations;

namespace ConsoleProject.NET.Services;

public class AuthService(
    AppDbContext dbContext,
    IJwtTokenGenerator jwtTokenGenerator
    ) : IAuthService
{
    // Возвращаем nullable, чтобы в контроллере не зависимо от проблем возвращать NotFound.
    // Таким образом, даже если login верный, пользователь со стороны не узнает, существует ли у нас этот пользователь.
    // Примерно по этой же причины для userId я использовал guid. В отличии от числа, мы не можем пройтись по ним по возрастанию,
    // чтобы проверить, какие пользователи есть в системе, а какие нет.
    // Возвращаем именно объект, чтобы в будущем кое-что доработать.
    public JwtTokenVm SignUp(SignUpDto dto)
    {
        var user = new User
        {
            Name = dto.UserName,
            Password = PasswordHasher.HashPassword(dto.Password),
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        var token = UpdateToken(user);
        dbContext.SaveChanges();
        // Сразу возвращаем токен после регистрации, чтобы можно было его использовать.
        return token.ToJwtTokenVm();
    }
    public JwtTokenVm? LogIn(LogInDto dto)
    {
        var user = dbContext.Users.FirstOrDefault(x => x.Name == dto.UserName);
        if (user is null)
            return null;
        // Сравниваем пароль из БД с переданным паролем.
        // НЕ ТЕРЯЕМ ВОСКЛИЦАТЕЛЬНЫЙ ЗНАК!
        if (!PasswordHasher.VerifyPassword(user.Password, dto.Password))
            return null;
        // Если все ок, обновляем токен.
        var token = UpdateToken(user);
        dbContext.SaveChanges();
        return token?.ToJwtTokenVm();
    }
    public bool LogOut(Guid userId)
    {
        var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
        if (user is null)
            return false;
        var token = dbContext.JwtTokens.FirstOrDefault(x => x.UserId == userId);
        if (token is null)
        {
            return false;
        }
        dbContext.Remove(token);
        dbContext.SaveChanges();
        return true;
    }

    public bool VerifyToken(Guid userId, string token)
    {
        var jwtToken = dbContext.JwtTokens.FirstOrDefault(x => x.UserId == userId);
        if (jwtToken is null)
            return false;
        return jwtToken.Token == token && jwtToken.ExpiresAt > DateTime.UtcNow;
    }
    private JwtToken UpdateToken(User user)
    {
        var token = jwtTokenGenerator.Generate(user);
        var oldToken = dbContext.JwtTokens.FirstOrDefault(t => t.UserId == user.Id);
        if (oldToken is not null)
        {
            dbContext.Remove(oldToken);
        }
        dbContext.JwtTokens.Add(token);
        return token;
    }
}

