using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Contract;

public record NoteAddDto(string Title, string Description, PriorityDto? priority, int UserId);

public record NoteUpdateDto(string? Title, string? Description, PriorityDto? priority, bool? IsCompleted);