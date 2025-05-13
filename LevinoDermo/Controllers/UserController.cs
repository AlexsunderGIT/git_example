using ConsoleProject.NET.DTO;
using ConsoleProject.NET.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Interfaces.Repositories;
using ConsoleProject.NET.DTO.User;
using ConsoleProject.NET.Interfaces.Services;
using ConsoleProject.NET.Services;

namespace ConsoleProject.NET.Controllers
{
    [ApiController]
    // указываем базовый путь, который будет обрабатывать этот контроллер;
    // [controller] - шаблон, говорящий, что мы обрабатываем пути с именем контроллера,
    // в нашем случае контроллер будет обрабатывать запросы с базовым путем "api/User"
    [Route("api/[controller]")]
    // наследуемся от Controller, чтобы получить доступ к базовой функциональности
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        private readonly IAuthService _authenticationService;

        public UserController (
            IUserService userService,
            IAuthService authenticationService)
        {
                _userService = userService;
                _authenticationService = authenticationService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await _userService.Register(request);
            return CreatedAtAction(nameof(GetUser), new {id = response.Id}, response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthentificationResponseDto>> Login (UserLoginDto request)
        {
            var response = await _authenticationService.Login(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            return NotFound("User not found");

            return Ok(user);
        }

    }
}
