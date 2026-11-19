using Api.DTOs;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterUserAsync(userRegisterDTO);
            if (result.Status == StatusEnum.UserAlreadyExists)
                return Conflict(result.Message);

            if (result.Status == StatusEnum.RegistrationComplete)
                return Ok(result.Message);
            throw new InvalidOperationException("Internal Server Error");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authResponse = await _userService.AuthenticateUserAsync(loginDTO);

            if (authResponse == null)
                return Unauthorized("Invalid Credentials.");

            SetAuthCookies(authResponse);

            return Ok("Login was successful.");
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return Unauthorized("Invalid refresh token.");

            var IsRevoked = await _userService.RevokeRefreshTokenAsync(refreshToken);
            if (!IsRevoked)
                return Unauthorized("Invalid refresh token.");

            ClearAuthCookies();
            return NoContent();
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return Unauthorized("Invalid refresh token.");

            var authResponse = await _userService.RefreshTokenAsync(refreshToken);
            if (authResponse == null)
                return Unauthorized("Invalid refresh token.");

            ClearAuthCookies();
            SetAuthCookies(authResponse);

            return NoContent();
        }

        private void SetAuthCookies(AuthResponseDTO authResponse)
        {
            Response.Cookies.Append("refreshToken", authResponse.RefreshToken);
            Response.Cookies.Append("accessToken", authResponse.AccessToken);
        }
        private void ClearAuthCookies()
        {
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Delete("accessToken");
        }
    }
}
