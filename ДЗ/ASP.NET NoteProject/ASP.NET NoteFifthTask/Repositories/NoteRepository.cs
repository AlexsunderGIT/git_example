using AutoMapper;
using ConsoleProject.NET.Abstractions;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Database;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly AppDbContext _dbContext;

    public NoteRepository(IUserRepository userRepository, AppDbContext dbContext, IMapper mapper)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public NoteVM? GetById(Guid id)
    {
        var note = _dbContext.Notes.FirstOrDefault(z => z.Id == id)
            ?? throw new NoteNotFoundException(id);
        return _mapper.Map<NoteVM>(note);
    }

    public IReadOnlyList<NoteVM> GetByUserId(Guid userId)
    {
        _userRepository.GetById(userId);
        var notes = _dbContext.Notes
            .Where(z  => z.UserId == userId)
            .ToList();
        return _mapper.Map<IReadOnlyList<NoteVM>>(notes);
    }
    public Guid Add(NoteAddDto dto)
    {
        _userRepository.GetById(dto.UserId);
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new TitleIsRequired();
        var note = _mapper.Map<Note>(dto);
        note.NoteCreationTime = DateTime.UtcNow;
        _dbContext.Notes.Add(note);
        _dbContext.SaveChanges();
        return note.Id;
    }
    public void Update(Guid id, NoteUpdateDto dto)
    {
        var note = _dbContext.Notes.FirstOrDefault(v => v.Id == id)
        ?? throw new NoteNotFoundException(id);
        _mapper.Map(dto, note);
        _dbContext.SaveChanges();
    }
    public void Delete(Guid id)
    {
        var note = _dbContext.Notes.FirstOrDefault(x => x.Id == id)
        ?? throw new NoteNotFoundException(id);
        _dbContext.Notes.Remove(note);
        _dbContext.SaveChanges();
    }
}