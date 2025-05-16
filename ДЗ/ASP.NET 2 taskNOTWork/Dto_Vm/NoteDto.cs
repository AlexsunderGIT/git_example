using ConsProj33.Models;

namespace ConsProj33.Dto_Vm
{
    public record NoteAddDto(string Title, string Description, Priority priority, int UserId);

    public record NoteUpdateDto(string? Title, string? Description, Priority? priority, bool? IsCompleted);
}