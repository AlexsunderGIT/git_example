using System.Security.Cryptography.X509Certificates;
using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.User;
using ConsoleProject.NET.Interfaces;
using ConsoleProject.NET.Interfaces.Services;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Utils;
using Microsoft.AspNetCore.Identity;

namespace ConsoleProject.NET.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserService (IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
    public async Task<UserResponseDto> 
    Register(UserRegisterDto request)
    {
        if (await _userRepository.UserExists(request.Name))
        throw new ApplicationException("Username already exists");

        _passwordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User
        {
            Name = request.Name.Trim(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        await _userRepository.Create(user);
        return new UserResponseDto(user.Id, user.Name);
    } 
    public async Task<UserResponseDto> 
    GetUserById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
        throw new ApplicationException("User not found");
        return new UserResponseDto (user.Id, user.Name);
    }
    }
}