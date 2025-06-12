using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using System.Collections.Generic;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace ConsoleProject.NET.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NoteController(INoteRepository noteRepository) : BaseController
{
    [HttpGet("user/{userId}")]
    public ActionResult<IReadOnlyList<NoteVM>> GetByUser(Guid userId)
    => Ok(noteRepository.GetByUserId(userId));
    [HttpGet("{id}")]
    public ActionResult<NoteVM> GetById(Guid id)
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
    public ActionResult Update(Guid id, NoteUpdateDto dto)
    {
        noteRepository.Update(id, dto);
        return NoContent();
    }
    [Authorize(Policy = "PostsOwner")]
    //[HttpDelete("{id:int}")]
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        noteRepository.Delete(id);
        return NoContent();
    }
}