using ConsProj33.Mapping;
using ConsProj33.Repositories;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<INoteRepository, NoteRepository>();
builder.Services.AddAutoMapper(typeof(NoteProfile));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
