using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public interface INoteRepository
{
    NoteVM? GetById(int id);
    IReadOnlyList<NoteVM> GetByUserId(int userId);
    int Add(NoteAddDto dto);
    void Delete(int id);
    void Update(int id, NoteUpdateDto dto);
}