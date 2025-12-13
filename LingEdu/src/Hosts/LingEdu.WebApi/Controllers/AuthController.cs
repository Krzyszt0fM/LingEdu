using LingEdu.Users.Application.Users;
using LingEdu.Users.Application.Users.LoginUser;
using LingEdu.Users.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LingEdu.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(request.Email, request.UserName, request.Password, request.Language);
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginUserCommand(request.Email, request.Password);
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result.Value) : Unauthorized(new { error = result.Error });
        }
    }
}