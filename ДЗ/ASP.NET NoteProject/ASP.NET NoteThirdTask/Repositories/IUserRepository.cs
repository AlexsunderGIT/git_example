using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public interface IUserRepository
{
    IReadOnlyList<User> GetUsers();
    User? GetById(int id);
    int Add(User user);
}