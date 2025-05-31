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
public class NoteController(INoteRepository noteRepository) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public ActionResult<IReadOnlyList<NoteVM>> GetByUser(int userId) 
    => Ok(noteRepository.GetByUserId(userId));
    [HttpGet("{id}")]
    public ActionResult<NoteVM> GetById(int id)
    {
        var note = noteRepository.GetById(id)
        ?? throw new NoteNotFoundException(id);
        return Ok(note);
    }
    [HttpPost]
    public ActionResult<NoteVM> Create(NoteAddDto dto)
    {
        var id = noteRepository.Add(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
    [HttpPut("{id}")]
    public ActionResult Update(int id, NoteUpdateDto dto)
    {
        noteRepository.Update(id, dto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        noteRepository.Delete(id);
        return NoContent();
    }
}