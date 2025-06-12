using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository repository, IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("signup")]
    // ¬озвращаем токен, чтобы сразу залогинитьс€ потом в Swagger.
    public ActionResult<JwtTokenVm> SignUp([FromBody] SignUpDto dto)
    {
        var token = authService.SignUp(dto);
        return Ok(token);
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<JwtTokenVm> LogIn([FromBody] LogInDto dto)
    {
        var result = authService.LogIn(dto);
        if (result is null)
        {
             return NotFound();
        }
        return Ok(result);
    }
 // Ќе говорим, где ошибка, чтобы не было угадываний имени пользовател€.
    [HttpPost("logout")]
    public ActionResult<bool> LogOut([FromBody] Guid userId)
    {
        var result = authService.LogOut(userId);
        if (!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
    //[HttpGet]
    //public ActionResult<IReadOnlyList<UserVm>> GetUsers()
    //{
    //    return Ok(repository.GetUsers());
    //}
    //[HttpPost]
    //public ActionResult<int> Create(UserAddDto dto)
    //{
    //    var id = repository.Add(dto);
    //    return CreatedAtAction(nameof(GetById), new { id }, id);
    //}
    //[HttpGet("{id}")]
    //public ActionResult<User> GetById(Guid id)
    //{
    //    var user = repository.GetById(id);
    //    return user != null ? Ok(user) : NotFound();
    //}
}