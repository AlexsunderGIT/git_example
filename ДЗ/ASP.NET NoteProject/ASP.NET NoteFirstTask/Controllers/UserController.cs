using ConsoleProject.NET.Models;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Exceptions;
using System.Xml.Linq;

namespace ConsoleProject.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> GetUsers() 
        {
            return Ok(DataStorage.Users);
        }
        [HttpPost]
        public ActionResult<int > AddUser([FromBody] UserRequest request)
        {
            var id = DataStorage.Users.Count;
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new NameIsRequired("Name is required");
            var user = new User { 
                Id = id,
                Name = request.Name,
                Password = request.Password
            };
            DataStorage.Users.Add(user);
            return CreatedAtAction(nameof(GetUsers), user);
        }
    }
    public class UserRequest
    {
        public string Name { get; set; } = null;
        public string Password { get; set; }
    }
}
