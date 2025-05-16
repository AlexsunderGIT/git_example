using ConsProj33.Models;

namespace ConsProj33.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetByUserId(int userId);
        Note? GetById(int id);
        int Add(Note note);
        void Delete(int id);
        void Update(Note note);
    }
}