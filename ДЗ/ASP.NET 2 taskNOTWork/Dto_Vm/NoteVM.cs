using ConsProj33.Models;

namespace ConsProj33.Dto_Vm
{
    public record NoteVM(int Id, string Title, string Description, DateTime NoteCreationTime, Priority priority, bool IsCompleted);
}