using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using EmpDepRoleFulstackProjectJun13.Repositories;
using EmpDepRoleFulstackProjectJun13.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmpDepRoleFulstackProjectJun13.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        public AuthController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService; 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var user = await _authService.ValidateUserAsync(loginDto.Username, loginDto.Password);

                if (user == null)
                    return Unauthorized("Invalid username or password.");

                if (string.IsNullOrWhiteSpace(user.Role))
                    return BadRequest("User must have a valid role.");

                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Log or inspect error (you can set a breakpoint here)
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }


    }
}
