using System.ComponentModel.DataAnnotations;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.DTO.Notes
{
    public class NoteResponseDto
    {
        internal object id;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime NoteCreationTime { get; /*private*/ set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get; set; }
        public NoteResponseDto (Note note)
        {
            Id = note.Id;
            Title = note.Title;
            Description = note.Description;
            NoteCreationTime = note.NoteCreationTime;
            IsCompleted = note.IsCompleted;
            Priority = note.Priority;
            UserId = note.UserId;
        }
    }
}