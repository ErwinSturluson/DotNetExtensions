using Microsoft.AspNetCore.Mvc;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Models;
using DotNetExtensions.Authorization.OAuth20.Server.Account.Client.Abstractions;

namespace DotNetExtensions.Authorization.OAuth20.Server.Account.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IEndUserAuthenticationService _endUserAuthenticationService;

        public AuthController(IEndUserAuthenticationService endUserAuthenticationService)
        {
            _endUserAuthenticationService = endUserAuthenticationService;
            // Initialize your controller here
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            await Task.CompletedTask;

            if (ModelState.IsValid)
            {
                // Replace this with your actual authentication logic
                var loginModelResult = await _endUserAuthenticationService.AuthenticateAsync(loginModel);

                if (loginModelResult.Successful)
                {
                    // Handle successful authentication
                    return Ok(new { Message = "Login successful" });
                }
                else
                {
                    // Handle failed authentication
                    return Unauthorized(new { Message = "Login failed" });
                }
            }

            // Return bad request for invalid model state
            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutModel logoutModel)
        {
            await Task.CompletedTask;

            if (ModelState.IsValid)
            {
                // Replace this with your actual authentication logic
                var loginModelResult = await _endUserAuthenticationService.LogoutAsync(logoutModel);

                if (loginModelResult.Successful)
                {
                    // Handle successful authentication
                    return Ok(new { Message = "Logout successful" });
                }
                else
                {
                    // Handle failed authentication
                    return Unauthorized(new { Message = "Logout failed" });
                }
            }

            // Return bad request for invalid model state
            return BadRequest(ModelState);
        }
    }
}
