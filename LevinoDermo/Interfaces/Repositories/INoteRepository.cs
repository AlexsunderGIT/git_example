using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Interfaces.Repositories
{
    public interface INoteRepository
    {
         Task<Note> Create(Note note);
         Task<IEnumerable<Note>> GetAllForUser(int userId);
         Task<Note?> GetById(int noteId);
         Task<Note> Update(Note note);
         Task Delete (Note note);
    }
}