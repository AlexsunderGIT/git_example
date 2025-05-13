using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.Notes;

namespace ConsoleProject.NET.Interfaces.Services
{
    public interface INoteService
    {
        Task<NoteResponseDto>
        Create(int userId, NoteCreateDto request);

        Task<IEnumerable<NoteResponseDto>>
        GetAll(int userId);
        Task<NoteResponseDto> GetNote(int noteId, int userId);

        Task<NoteResponseDto>
        Update(int noteId, int userId, NoteUpdateDto request);

        Task<bool> Delete(int noteId, int userId);
    }
}