using ConsoleProject.NET.DTO;
using ConsoleProject.NET.DTO.Notes;
using ConsoleProject.NET.Interfaces.Repositories;
using ConsoleProject.NET.Interfaces.Services;
using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConsoleProject.NET.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository _noteRepository)
        {
            _noteRepository = _noteRepository; 
        }
        public async Task<NoteResponseDto> Create (int userId, NoteCreateDto request)
        {
            var note = new Note
            {
                Title = request.Title?.Trim(),
                Description = request.Description?.Trim(),
                Priority = request.Priority,
                UserId = userId,
                NoteCreationTime = DateTime.UtcNow,
            };
            var createdNote = await _noteRepository.Create(note);
            return new NoteResponseDto(createdNote);
        }
        public async Task<IEnumerable<NoteResponseDto>> GetAll (int userId)
        {
            var notes = await _noteRepository.GetAllForUser(userId);
            return notes.Select(x => new NoteResponseDto(x));
        }
        public async Task<NoteResponseDto> GetNote(int noteId, int userId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null || note.UserId != userId)
            return null;
            return new NoteResponseDto(note);
        }
        public async Task<NoteResponseDto?> Update(int noteId, int userId, NoteUpdateDto request)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null || note.UserId != userId)
            return null;
            note.Title = request.Title ?? note.Title;
            note.Description = request.Description ?? note.Description;
            note.IsCompleted = request.IsCompleted ?? note.IsCompleted;
            note.Priority = request.Priority;
            
            var updatedNote = await _noteRepository.Update(note);
            return new NoteResponseDto(updatedNote);
        }
        public async Task<bool> Delete(int noteId, int userId)
        {
            var note = await _noteRepository.GetById(noteId);
            if (note == null || note.UserId != userId)
            return false;
            
            await _noteRepository.Delete(note);
            return true;
        }
    }
}