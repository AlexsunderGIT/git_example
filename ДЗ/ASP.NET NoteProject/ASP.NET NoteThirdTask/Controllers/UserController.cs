using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Contract;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<UserVm>> GetUsers()
    {
        return Ok(repository.GetUsers());
    }
    [HttpPost]
    public ActionResult<int> Create(UserAddDto dto)
    {
        var id = repository.Add(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id) 
    {
        var user = repository.GetById(id);
        return user != null ? Ok(user) : NotFound();
    }
}