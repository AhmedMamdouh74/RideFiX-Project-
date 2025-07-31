using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.CoreServicesAbstractions.Account;
using SharedData.DTOs.Account;

namespace RideFix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            this._authService = authService;
        }
        [HttpPost("register-step1")]

        public async Task<IActionResult> registerStep1([FromBody] RegisterStep1Dto step1Dto)
        {

            var result = await _authService.RegisterStep1Async(step1Dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "Step 1 completed successfully. Proceed to Step 2." });

        }
        [HttpPost("register-step2")]

        public async Task<IActionResult> registerStep2([FromForm] RegisterStep2Dto step2Dto)
        {

            var result = await _authService.RegisterStep2Async(step2Dto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "Registration completed successfully." });

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Invalid email or password");

            return Ok(new { token });
        }
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var isEmailExists = await _authService.CheckEmailExists(email);
            return Ok(isEmailExists);
        } 



    }
}
