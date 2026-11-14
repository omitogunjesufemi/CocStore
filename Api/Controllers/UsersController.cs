using Api.DTOs;
using Api.Services.Interfaces;
using Api.Services.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterUserAsync(userRegisterDTO);
            if (result.Status == StatusEnum.UserAlreadyExists)
                return BadRequest(result.Message);

            if (result.Status == StatusEnum.RegistrationComplete)
                return Ok(new
                {
                    Message = result.Message,
                });
            throw new InvalidOperationException("Internal Server Error");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authResponse = await _userService.AuthenticateUserAsync(loginDTO);

            if (authResponse == null)
                return Unauthorized("Invalid Credentials.");

            return Ok(authResponse);
        }
    }
}
