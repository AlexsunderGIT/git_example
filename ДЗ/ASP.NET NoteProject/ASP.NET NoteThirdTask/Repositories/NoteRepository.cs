    using ConsoleProject.NET.Contract;
    using ConsoleProject.NET.Exceptions;
    using ConsoleProject.NET.Models;

    namespace ConsoleProject.NET.Repositories;

    public class NoteRepository : INoteRepository
    {
        private readonly List<Note> _notes = new();
        private int _idCounter;
        //
        private readonly IUserRepository _userRepository;
    
        public NoteRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //
        public IEnumerable<Note> GetByUserId(int userId)
        {
        //
            if (_userRepository.GetById(userId) == null)
                throw new UserNotFoundException();
            return _notes.Where(x => x.UserId == userId);
        //
        //_notes.Where(x => x.UserId == userId);
        }

        public Note? GetById(int id)
        {
        //
         var note = _notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException();
            }
            return note;
        //
        //_notes.FirstOrDefault(x => x.Id == id);
    }

    public int Add(Note note)
        {
            //
            if (_userRepository.GetById(note.UserId) == null)
                throw new UserNotFoundException();
            if (string.IsNullOrWhiteSpace(note.Title))
                throw new TitleIsRequired();
            //
            note.Id = _idCounter++;
            //note.NoteCreationTime = DateTime.Now;
            _notes.Add(note);
            return note.Id;
        }
        public void Update(Note note)
        {
            //
            if (note == null)
                throw new NoteNotFoundException();
            //
            var index = _notes.FindIndex(x => x.Id == note.Id);
            if (index != -1) _notes[index] = note;
        }
        public void Delete(int id)
        {
            //
            var note = _notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException();
            }
            _notes.Remove(note);
            //
            //_notes.RemoveAll(x => x.Id == id);
        }
    }
