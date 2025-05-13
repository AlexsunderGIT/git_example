using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.User;

namespace ConsoleProject.NET.Interfaces.Services
{
    public interface IAuthService
    {
         Task<AuthentificationResponseDto>
         Login(UserLoginDto request);
    }
}