using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _service.GetAllUsers();
            return Ok(users);

        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var newId = _service.CreateUser(user);
            return Ok(new { Id = newId });
        }

    }

}