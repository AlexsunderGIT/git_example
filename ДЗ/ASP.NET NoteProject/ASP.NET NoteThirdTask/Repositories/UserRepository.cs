using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class UserRepository : IUserRepository   
{
    private readonly List<User> _users = new();
    private int _idCounter;
    public IReadOnlyList<User> GetUsers() => _users;
    public User? GetById(int id) 
    {
        //
        var user = _users.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        return user;
    }
    public int Add(User user)
    {
        //
        if (string.IsNullOrWhiteSpace(user.Name))
            throw new NameIsRequired();
        //
        user.Id = ++_idCounter;
        _users.Add(user);
        return user.Id;
    }
}