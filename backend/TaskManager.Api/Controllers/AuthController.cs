using Microsoft.AspNetCore.Mvc;

using TaskManager.Api.Services;
using LoginRequest = TaskManager.Api.Models.LoginRequest;
using ResetPasswordRequest = TaskManager.Api.Models.ResetPasswordRequest;


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


        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var success = _authService.ResetPassword(request.Email, request.NewPassword);

            if (!success)
            {
                return NotFound(new { message = "No account found with that email" });
            }

            return Ok(new { message = "Password updated successfully" });
        }

    }
}

