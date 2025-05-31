using AutoMapper;
using ConsoleProject.NET.Contract;
    using ConsoleProject.NET.Exceptions;
    using ConsoleProject.NET.Models;

    namespace ConsoleProject.NET.Repositories;

public class NoteRepository(IUserRepository userRepository, IMapper mapper) : INoteRepository
{
    private readonly List<Note> _notes = new();
    private int _idCounter;
    private readonly IMapper _mapper = mapper;
    public NoteVM? GetById(int id)
    {
        var note = _notes.FirstOrDefault(z => z.Id == id)
            ?? throw new NoteNotFoundException(id);
        return _mapper.Map<NoteVM>(note);
    }

    public IReadOnlyList<NoteVM> GetByUserId(int userId)
    {
        userRepository.GetById(userId);
        return _mapper.Map<IReadOnlyList<NoteVM>>(_notes.Where(o => o.UserId == userId));
    }
    public int Add(NoteAddDto dto)
    {
        userRepository.GetById(dto.UserId);
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new TitleIsRequired();
        var note = _mapper.Map<Note>(dto);
        note.Id = _idCounter++;
        _notes.Add(note);
        return note.Id;
    }
    public void Update(int id, NoteUpdateDto dto)
    {
        var note = _notes.FirstOrDefault(v => v.Id == id)
        ?? throw new NoteNotFoundException(id);
        _mapper.Map(dto, note);
    }
    public void Delete(int id)
    {
        var note = _notes.FirstOrDefault(x => x.Id == id)
        ?? throw new NoteNotFoundException(id);
        _notes.Remove(note);
    }
}