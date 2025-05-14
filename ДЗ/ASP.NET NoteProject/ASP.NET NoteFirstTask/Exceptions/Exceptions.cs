namespace ConsoleProject.NET.Exceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException() : base($"Note not found") { }
    }
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base($"User not found") { }
    }

}