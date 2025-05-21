namespace ConsoleProject.NET.Exceptions;

public class NoteNotFoundException : Exception
{
    public NoteNotFoundException() : base($"Note not found") { }
}
public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base($"User not found") { }
}
public class NameIsRequired : Exception
{
    public NameIsRequired() : base($"Name is required") { }
}
public class TitleIsRequired : Exception
{
    public TitleIsRequired() : base($"Title is required") { }
}