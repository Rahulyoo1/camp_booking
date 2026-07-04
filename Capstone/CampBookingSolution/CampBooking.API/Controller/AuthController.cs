using CampBooking.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CampBooking.Business.Interfaces;
using CampBooking.Domain.DTOs.AuthDto;
using Microsoft.AspNetCore.Mvc;

namespace CampBooking.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await authService.RegisterAsync(request);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await authService.LoginAsync(request);
            return Ok(result);
        }
    
    }
}
