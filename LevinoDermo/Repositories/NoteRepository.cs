using System.Collections.Concurrent;
using ConsoleProject.NET.Interfaces.Repositories;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ConcurrentDictionary<int, Note> _notes = new();
        private int _idCounter;
        public Task<Note> Create (Note note)
        {
            note.Id = Interlocked.Increment(ref _idCounter);
            _notes.TryAdd(note.Id, note);
            return Task.FromResult(note);
        }
        public Task<IEnumerable<Note>> GetAllForUser(int userId)
        {
            return Task.FromResult(_notes.Values.Where(x => x.UserId == userId));
        }
        public Task<Note?> GetById(int noteId)
        {
            _notes.TryGetValue(noteId, out var note);
            return Task.FromResult(note);
        }
        public Task Delete(Note note)
        {
            _notes.TryRemove(note.Id, out _);
            return Task.CompletedTask;
        }

        public Task<Note> Update(Note note)
        {
            _notes[note.Id] = note;
            return Task.FromResult(note);
        }
    }
}