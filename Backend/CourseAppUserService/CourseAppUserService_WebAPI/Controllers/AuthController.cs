using CourseAppUserService_Application.Users.Commands.LoginUser;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

public class AuthController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var (jwt, refresh) = await _mediator.Send(command);
        return Ok(new { jwt, refresh });
    }
}