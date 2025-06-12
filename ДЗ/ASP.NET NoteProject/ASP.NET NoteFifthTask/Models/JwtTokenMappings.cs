using ConsoleProject.NET.Contract;

namespace ConsoleProject.NET.Models;

// Так как тут partial, мы можем объявление одного класса разнести по нескольким файлам.
// Вряд ли это когда-то имеет смысл кроме маппингов.
public partial class JwtToken
{
    public JwtTokenVm ToJwtTokenVm()
    {
        return new JwtTokenVm(UserId, Token, ExpiresAt);
    }
}