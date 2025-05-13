using ConsoleProject.NET.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Collections.Concurrent;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<int, User> _users = new(); 
        private int _idCounter;
        public Task<User> Create (User user)
        {
            user.Id = Interlocked.Increment(ref _idCounter);
            _users.TryAdd(user.Id, user);
            return Task.FromResult(user);
        }
        public Task<User> GetByName(string name)
        {
            return Task.FromResult(_users.Values.FirstOrDefault(x => x.Name == name));
        }
        public Task<bool> UserExists(string name)
        {
            return Task.FromResult(_users.Values.Any(x => x.Name == name));
        }
        public Task<User> GetById(int id)
        {
            _users.TryGetValue(id, out var user);
            return Task.FromResult(user);
        }
    }
}