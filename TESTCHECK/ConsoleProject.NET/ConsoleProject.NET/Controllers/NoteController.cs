using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.Mvc;

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
        private static readonly List<Note> notes = new();
        [HttpGet]
        public ActionResult<List<Note>> GetNotes(int userId, [AsParameters] FiltredSortedNote sortAndFilter)
        {
            var users = new List<User>();
            if (!users.Any(Z => Z.Id == userId))
                return NotFound("user not found");
            var filter = notes.Where(O => O.UserId == userId);
            if (sortAndFilter.Priority.HasValue)
                filter = filter.Where(G => G.Priority == sortAndFilter.Priority.Value);
            if (sortAndFilter.IsCompleted.HasValue)
                filter = filter.Where(G => G.IsCompleted == sortAndFilter.IsCompleted.Value);
            if (!string.IsNullOrEmpty(sortAndFilter.DataSort))
            {
                var propertyName = sortAndFilter.DataSort.ToLower();
                filter = propertyName switch
                {
                    "priority" => sortAndFilter.SortDescending
                        ? filter.OrderByDescending(x => x.Priority)
                        : filter.OrderBy(x => x.Priority),
                    "iscompleted" => sortAndFilter.SortDescending
                        ? filter.OrderByDescending(x => x.IsCompleted)
                        : filter.OrderBy(x => x.IsCompleted),
                    "notecreationtime" => sortAndFilter.SortDescending
                        ? filter.OrderByDescending(x => x.NoteCreationTime)
                        : filter.OrderBy(x => x.NoteCreationTime),
                    _ => filter
                };
            }
            return Ok(filter.ToList());
        }
        [HttpPost]
        public ActionResult<int> Add([FromBody] NoteRequest request)
        {
            var noteId = notes.Count;
            var users = new List<User>();
            var user = users.FirstOrDefault(V => V.Id == request.UserId);
            if (user == null)
                return NotFound("user not found");
            var note = new Note
            {
                Id = notes.Count,
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                Priority = request.Priority,
                NoteCreationTime = DateTime.Now,
            };
            notes.Add(note);
            return Created($"/api/Note/{note.Id}", note);
        }
    }
}
