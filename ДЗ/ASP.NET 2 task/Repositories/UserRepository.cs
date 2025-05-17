using ConsProj33.Models;

namespace ConsProj33.Repositories
{
    public class UserRepository : IUserRepository   
    {
        private readonly List<User> _users = new();
        private int _idCounter;
        public IEnumerable<User> GetUsers() => _users;
        public User? GetById(int id) => _users.FirstOrDefault(x => x.Id == id);
        public int Add(User user)
        {
            user.Id = ++_idCounter;
            _users.Add(user);
            return user.Id;
        }
    }
}