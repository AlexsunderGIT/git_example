using Microsoft.AspNetCore.Builder;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using ConsoleProject.NET.Configurations.Mapping;
using SimpleExample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddApplicationServices();

var app = builder.Build();
app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
// Потом авторизация.
app.UseAuthorization();
// И только потом контроллеры.
app.MapControllers();

app.Run();
