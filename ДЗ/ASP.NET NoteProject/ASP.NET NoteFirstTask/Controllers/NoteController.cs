using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Exceptions;

namespace ConsoleProject.NET.Controllers
{
    [ApiController]
    // указываем базовый путь, который будет обрабатывать этот контроллер;
    // [controller] - шаблон, говорящий, что мы обрабатываем пути с именем контроллера,
    // в нашем случае контроллер будет обрабатывать запросы с базовым путем "api/User"
    [Route("api/[controller]")]
    // наследуемся от Controller, чтобы получить доступ к базовой функциональности
    public class NoteController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Note>> GetNotes()
        {
            return Ok(DataStorage.Notes);
        }
        [HttpPost]
        public ActionResult<int> Add([FromBody] NoteRequest request)
        {
            var noteId = DataStorage.Notes.Count;
            var note = new Note
            {
                Id = DataStorage.Notes.Count,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                IsCompleted = false,
                NoteCreationTime = DateTime.Now
            };
            DataStorage.Notes.Add(note);
            return CreatedAtAction(nameof(GetNotes), note);
        }
        [HttpPut("{id}")]
        public ActionResult<int> Update(int id, [FromBody] NoteUpdateRequest request)
        {
            var note = DataStorage.Notes.FirstOrDefault(notes => notes.Id == id);
            if (note == null)
            {
                throw new UserNotFoundException();
            }
            var originalNote = new Note
            {
                Id = note.Id,
                Title = note.Title,
                Description = note.Description,
                NoteCreationTime = note.NoteCreationTime,
                IsCompleted = note.IsCompleted,
                Priority = note.Priority
            };
            note.Title = request.Title ?? note.Title;
            note.Description = request.Description ?? note.Description;
            note.IsCompleted = request.IsCompleted ?? note.IsCompleted;
            note.Priority = request.Priority ?? note.Priority;
            return Ok(new
            {
                UpdatedNote = note,
                OriginalNote = originalNote
            });
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var note = DataStorage.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException();
            }

            DataStorage.Notes.Remove(note);
            return NoContent(); 
        }
        public class NoteRequest
        {
            public string Title { get; set; } = null;
            public string Description { get; set; } = null;
            public Priority Priority { get; set; }
            public int UserId { get; set; }
        }
        public class NoteUpdateRequest
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public bool? IsCompleted { get; set; }
            public Priority? Priority { get; set; }
        }
    }
}
