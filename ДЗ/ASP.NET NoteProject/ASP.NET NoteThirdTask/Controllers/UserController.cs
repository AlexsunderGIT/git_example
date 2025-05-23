using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Contract;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public ActionResult<IReadOnlyList<User>> GetUsers()
    {
        return Ok(_repository.GetUsers());
    }
    [HttpPost]
    public ActionResult<int> Create([FromBody] UserAddDto dto)
    {
        var user = new User { Name = dto.Name, Password = dto.Password };
        var id = _repository.Add(user);
        
        //if (string.IsNullOrWhiteSpace(dto.Name))
        //    throw new NameIsRequired();
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id) 
    {
        var user = _repository.GetById(id);
        return user != null ? Ok(user) : NotFound();
    }
}