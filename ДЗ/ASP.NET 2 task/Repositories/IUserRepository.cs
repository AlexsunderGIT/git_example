using ConsProj33.Models;

namespace ConsProj33.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User? GetById(int id);
        int Add(User user);
    }
}