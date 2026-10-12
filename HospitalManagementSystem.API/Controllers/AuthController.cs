using HospitalManagementSystem.API.DTOs;
using HospitalManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            try
            {
                _userService.Register(registerDto);

                return Ok(new
                {
                    message = "User registered successfully."
                });
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email is already registered.")
                {
                    return Conflict(new { message = ex.Message });
                }

                return StatusCode(500, new
                {
                    message = "An error occurred during registration."
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                var result = _userService.Login(loginDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Invalid email or password.")
                {
                    return Unauthorized(new { message = ex.Message });
                }

                return StatusCode(500, new
                {
                    message = "An error occurred during login."
                });
            }
        }
    }
}
