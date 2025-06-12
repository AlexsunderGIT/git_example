namespace ConsoleProject.NET.Exceptions;

public class NoteNotFoundException : Exception
{
    public NoteNotFoundException(Guid id) : base($"Note not found") { }
}
public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id) : base($"User not found") { }
}
public class NameIsRequired : Exception
{
    public NameIsRequired() : base($"Name is required") { }
}
public class TitleIsRequired : Exception
{
    public TitleIsRequired() : base($"Title is required") { }
}