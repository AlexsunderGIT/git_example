using System.ComponentModel.DataAnnotations;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.DTO
{
    public class NoteCreateDto
    {
        [Required(ErrorMessage = "Title required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 50 character in length.")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Priority required")]
        public Priority Priority { get; set; }

    }
}