using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.AssignRole;
using CourseAppUserService_Application.Users.Commands.LoginUser;
using CourseAppUserService_Application.Users.Commands.RefreshToken;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

public class AuthController(IMediator mediator, ILoggerService logger) : BaseController(mediator, logger)
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        Logger.Information("Registering user");
        var userId = await Mediator.Send(command);
        
        Logger.Information($"User {userId} registered successfully");
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        logger.Information("Authorizing user");
        var (jwt, refresh) = await Mediator.Send(command);
        
        logger.Information($"User {command.Email} logged in");
        return Ok(new { jwt, refresh });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
    {
        Logger.Information($"Assigning {command.Role} role for {command.Email}");
        var result = await Mediator.Send(command);
        
        Logger.Information("Role assigned successfully");
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        Logger.Information("Refreshing jwt token");
        var jwt = await Mediator.Send(command);
        
        Logger.Information("Jwt refreshed successfully");
        return Ok(jwt);
    }
}