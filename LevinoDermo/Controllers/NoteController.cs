using System.Security.Claims;
using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.Notes;
using ConsoleProject.NET.Interfaces.Services;
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
        private readonly INoteService _noteService;

        public NoteController(INoteService _noteService)
        {
            _noteService = _noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteResponseDto>>> GetAll()
        {
            var userId = GetCurrentUserId();
            var notes = await _noteService.GetAll(userId);
            return Ok(notes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteResponseDto>> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var note = await _noteService.GetAll(userId);
            if (note == null)
                return NotFound();

            return Ok(note);
        }
        [HttpPost]
        public async Task<ActionResult<NoteResponseDto>> Create(NoteCreateDto request)
        {
            var userId = GetCurrentUserId();
            var note = await _noteService.Create(userId, request);
            return CreatedAtAction(nameof(GetById), new { id = note.id }, note);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<NoteResponseDto>> Update(int id, NoteUpdateDto request)
        {
            var userId = GetCurrentUserId();
            var updatedNote = await _noteService.Update(id, userId, request);
            if (updatedNote == null)
                return NotFound();

            return Ok(updatedNote);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _noteService.Delete(id, userId);
            if (!result)
                return NotFound();

            return NoContent();
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("Токен не содержит идентификатора пользователя");

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new InvalidOperationException("Идентификатор пользователя в токене неверного формата");
            }

            return userId;
        }


    }
}
