using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;

        public TasksController(TaskService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public IActionResult GetTasksByUserId(int userId)
        {
            var taskItem = _service.GetTasksByUserId(userId);
            return Ok(taskItem);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            var newId = _service.CreateTask(task);
            return Ok(new { Id = newId });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateTaskStatus(int id, [FromBody] string newStatus)
        {
            _service.UpdateTaskStatus(id, newStatus);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteTask(int id)
        {
            _service.DeleteTask(id);
            return Ok();
        }
    }
}