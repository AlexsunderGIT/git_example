using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.User;
namespace ConsoleProject.NET.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> 
        Register(UserRegisterDto request);
   
        Task<UserResponseDto> GetUserById(int id);
    }
}