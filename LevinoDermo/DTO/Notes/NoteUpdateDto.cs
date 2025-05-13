using System.ComponentModel.DataAnnotations;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.DTO.Notes
{
    public class NoteUpdateDto
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 50 character in length.")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public Priority Priority { get; set; }
    }
}