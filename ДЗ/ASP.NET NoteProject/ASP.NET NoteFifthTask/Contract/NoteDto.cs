namespace ConsoleProject.NET.Contract;

public record NoteAddDto(string Title, string Description, PriorityDto? priority, Guid UserId);
public record NoteUpdateDto(string? Title, string? Description, PriorityDto? priority, bool? IsCompleted);
public record NoteVM(Guid Id, string Title, string Description, DateTime NoteCreationTime, PriorityDto? priority, bool IsCompleted);