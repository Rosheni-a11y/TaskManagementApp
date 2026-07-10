using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Logi([FromBody] LoginRequest request)
        {
            var token = _authService.Login(request.Email, request.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Ïnvalid email or password" });
            }

            return Ok(new { token });

        }
        
    }
}

