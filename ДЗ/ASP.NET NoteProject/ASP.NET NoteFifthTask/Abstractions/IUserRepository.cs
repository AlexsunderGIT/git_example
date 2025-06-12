using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.Hosting.Server;

namespace ConsoleProject.NET.Abstractions;

public interface IUserRepository
{
    IReadOnlyList<UserVm> GetUsers();
    UserVm? GetById(Guid id);
    Guid Add(UserAddDto dto);
}