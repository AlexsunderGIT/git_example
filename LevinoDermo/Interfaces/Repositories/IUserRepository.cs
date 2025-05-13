using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.User;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Interfaces.Services
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetByName(string name);
        Task<User> GetById(int id);
        Task<bool> UserExists(string name);
    }
}