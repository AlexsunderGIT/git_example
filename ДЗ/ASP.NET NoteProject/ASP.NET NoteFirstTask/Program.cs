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
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();
app.UseExceptionHandler("/error");
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
app.MapControllers();

app.Run();
