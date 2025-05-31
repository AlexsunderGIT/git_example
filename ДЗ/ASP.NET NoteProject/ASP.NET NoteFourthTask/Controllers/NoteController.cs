using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using System.Collections.Generic;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Contract;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public NoteController(INoteRepository noteRepository, IUserRepository userRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet("user/{userId}")]
    public ActionResult<IReadOnlyList<NoteVM>> GetByUser(int userId)
    {
        var notes = _noteRepository.GetByUserId(userId);
        return Ok(_mapper.Map<IReadOnlyList<NoteVM>>(notes));
    }
    [HttpPost]
    public ActionResult<NoteVM> Create(NoteAddDto dto)
    {
        var note = _mapper.Map<Note>(dto);
        var id = _noteRepository.Add(note);
        return Ok(id);
    }
    [HttpPut("{id}")]
    public ActionResult Update(int id, NoteUpdateDto dto)
    {
        var note = _noteRepository.GetById(id);

        _mapper.Map(dto, note);
        _noteRepository.Update(note);
        return NoContent();
    }
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        _noteRepository.Delete(id);
        return NoContent();
    }
}