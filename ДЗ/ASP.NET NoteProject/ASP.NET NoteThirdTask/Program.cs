using Microsoft.AspNetCore.Builder;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<INoteRepository, NoteRepository>();
builder.Services.AddAutoMapper(typeof(NoteProfile));

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
