using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class UserRepository(IMapper mapper) : IUserRepository   
{
    private readonly IMapper _mapper = mapper;
    private readonly List<User> _users = new();
    private int _idCounter;
    public UserVm GetById(int id)
    {
        var user = _users.FirstOrDefault(x => x.Id == id)
        ?? throw new UserNotFoundException(id);
        return _mapper.Map<UserVm>(user);
    }
    public IReadOnlyList<UserVm> GetUsers() => _mapper.Map<IReadOnlyList<UserVm>>(_users);
    public int Add(UserAddDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new NameIsRequired();
        var user = _mapper.Map<User>(dto);
        user.Name = user.Name?.Trim();
        user.Password = user.Password?.Trim();
        user.Id = ++_idCounter;
        _users.Add(user);
        return user.Id;
    }
}