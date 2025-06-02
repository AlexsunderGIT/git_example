using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Database;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class UserRepository(IMapper mapper, AppDbContext dbContext) : IUserRepository
{
    private readonly IMapper _mapper = mapper;
    private readonly AppDbContext _dbContext = dbContext;

    public UserVm GetById(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == id)
        ?? throw new UserNotFoundException(id);
        return _mapper.Map<UserVm>(user);
    }
    public IReadOnlyList<UserVm> GetUsers()
    {
        var users = _dbContext.Users.ToList();
        return _mapper.Map<IReadOnlyList<UserVm>>(users);
    }
    public int Add(UserAddDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new NameIsRequired();
        var user = _mapper.Map<User>(dto);
        user.Name = user.Name?.Trim();
        user.Password = user.Password?.Trim();
        _dbContext.Add(user);
        _dbContext.SaveChanges();
        return user.Id;
    }
}