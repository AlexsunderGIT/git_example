using ConsProj33.Models;

namespace ConsProj33.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly List<Note> _notes = new();
        private int _idCounter;

        public IEnumerable<Note> GetByUserId(int userId)
        => _notes.Where(x => x.UserId == userId);

        public Note? GetById(int id)
        => _notes.FirstOrDefault(x => x.Id == id);

        public int Add(Note note)
        {
            note.Id = _idCounter++;
            note.NoteCreationTime = DateTime.Now;
            _notes.Add(note);
            return note.Id;
        }
        public void Update(Note note)
        {
            var index = _notes.FindIndex(x => x.Id == note.Id);
            if (index != -1) _notes[index] = note;
        }
        public void Delete(int id)
        => _notes.RemoveAll(x => x.Id == id);
    }
}