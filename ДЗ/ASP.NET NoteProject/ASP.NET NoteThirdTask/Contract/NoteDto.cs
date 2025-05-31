using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Contract;

public record NoteAddDto(string Title, string Description, PriorityDto? priority, int UserId);

public enum PriorityDto
{
    Low = 0,
    Medium = 1,
    High = 2
}

public record NoteUpdateDto(string? Title, string? Description, PriorityDto? priority, bool? IsCompleted);