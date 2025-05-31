using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.Hosting.Server;

namespace ConsoleProject.NET.Repositories;

public interface IUserRepository
{
    IReadOnlyList<UserVm> GetUsers();
    UserVm? GetById(int id);
    int Add(UserAddDto dto);
}