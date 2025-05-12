using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.Mvc;

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
        private static readonly List<User> users = new();

        [HttpGet]
        public ActionResult<List<User>> GetUsers() 
        { 
            return Ok(users);
        }
        [HttpPost]
        public ActionResult<int > AddUser([FromBody] UserRequest request)
        {
            var id = users.Count;
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name is required");
            var user = new User { Id = id, Name = request.Name };
            users.Add(user);
            return Ok(id);
        }
    }
}
