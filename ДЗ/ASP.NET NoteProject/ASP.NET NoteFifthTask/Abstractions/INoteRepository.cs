using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Abstractions;

public interface INoteRepository
{
    NoteVM? GetById(Guid id);
    IReadOnlyList<NoteVM> GetByUserId(Guid userId);
    Guid Add(NoteAddDto dto);
    void Delete(Guid id);
    void Update(Guid id, NoteUpdateDto dto);
}