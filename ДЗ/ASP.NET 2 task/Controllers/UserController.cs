using ConsProj33.Models;
using ConsProj33.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConsProj33.Dto_Vm
{
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
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_repository.GetUsers());
        }
        [HttpPost]
        public ActionResult<int> Create([FromBody] UserAddDto dto)
        {
            var user = new User { Name = dto.Name, Password = dto.Password };
            var id = _repository.Add(user);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id) 
        {
            var user = _repository.GetById(id);
            return user != null ? Ok(user) : NotFound();
        }
    }
}