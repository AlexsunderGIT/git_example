using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ConsoleProject.NET.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.MapControllers();

app.Run();


//var userCounter = 0;
//var noteCounter = 0;

//var users = new List<User>();
//var notes = new List<Note>();
/*
app.MapGet("/user", () => users);

app.MapPost("/user", ([FromBody] UserRequest request)
 =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest("Name is required");
    var user = new User { Id = userCounter++, Name = request.Name };
    users.Add(user);
    return Results.Created($"/user/{user.Id}", user);
});

app.MapGet("/user/{user.Id}/notes", (int userId, [AsParameters] FiltredSortedNote sortAndFilter) =>
{
    if (!users.Any(Z => Z.Id == userId))
        return Results.NotFound("user not found");
    var filter = notes.Where(O => O.UserId == userId);
    if (sortAndFilter.Priority.HasValue)
        filter = filter.Where(G => G.Priority == sortAndFilter.Priority.Value);
    if (sortAndFilter.IsCompleted.HasValue)
        filter = filter.Where(G => G.IsCompleted == sortAndFilter.IsCompleted.Value);
    if (!string.IsNullOrEmpty(sortAndFilter.DataSort))
    {
        var propertyName = sortAndFilter.DataSort.ToLower();
        filter = propertyName switch
        {
            "priority" => sortAndFilter.SortDescending
                ? filter.OrderByDescending(x => x.Priority)
                : filter.OrderBy(x => x.Priority),
            "iscompleted" => sortAndFilter.SortDescending
                ? filter.OrderByDescending(x => x.IsCompleted)
                : filter.OrderBy(x => x.IsCompleted),            
            "notecreationtime" => sortAndFilter.SortDescending
                ? filter.OrderByDescending(x => x.NoteCreationTime)
                : filter.OrderBy(x => x.NoteCreationTime),
            _ => filter
        };
    }
    return Results.Ok(filter.ToList());
});

app.MapPost("/note", ([FromBody] NoteRequest request) =>
{
    var user = users.FirstOrDefault(V => V.Id == request.UserId);
    if (user == null)
        return Results.NotFound("user not found");
    var note = new Note
    {
        Id = noteCounter++,
        Title = request.Title,
        Description = request.Description,
        UserId = request.UserId,
        Priority = request.Priority,
        NoteCreationTime = DateTime.Now,
    };
    notes.Add(note);
    return Results.Created($"/user/{note.Id}", note);
});

app.MapPut("/note", (int id, [FromBody] NoteUpdateRequest request) =>
{
    var note = notes.FirstOrDefault(notes => notes.Id == id);
    if (note == null)
    {
        return Results.NotFound();
    }
    var originalNote = new Note
    {
        Id = note.Id,
        Title = note.Title,
        Description = note.Description,
        NoteCreationTime = note.NoteCreationTime,
        IsCompleted = note.IsCompleted,
        UserId = note.UserId,
        Priority = note.Priority
    };
    note.Title = request.Title ?? note.Title;
    note.Description = request.Description ?? note.Description;
    note.IsCompleted = request.IsCompleted ?? note.IsCompleted;
    note.Priority = request.Priority ?? note.Priority;
    return Results.Ok(new
    {
        UpdatedNote = note,
        OriginalNote = originalNote
    });
});

app.MapDelete("/note/{id}", (int id) =>
{
    var note = notes.FirstOrDefault(notes => notes.Id == id);
    if (note == null)
    {
        return Results.NotFound();
    }
    notes.Remove(note);
    return Results.NoContent();
});


public class UserRequest
{
    public string Name { get; set; } = null;
}
public class NoteRequest
{
    public string Title { get; set; } = null;
    public string Description { get; set; } = null;
    public Priority Priority { get; set; }
    public int UserId { get; set; }
}
public class NoteUpdateRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public Priority? Priority { get; set; }
}
public class FiltredSortedNote
{
    public Priority? Priority { get; set; }
    public bool? IsCompleted { get; set; }
    public string? DataSort { get; set; }
    public bool SortDescending { get; set; } = false;
}
*/