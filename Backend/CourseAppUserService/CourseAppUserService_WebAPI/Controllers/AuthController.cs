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
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken token)
    {
        Logger.Information("Registering user");
        var userId = await Mediator.Send(command, token);
        
        Logger.Information($"User {userId} registered successfully");
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command, CancellationToken token)
    {
        logger.Information("Authorizing user");
        var (jwt, refresh) = await Mediator.Send(command, token);
        
        logger.Information($"User {command.Email} logged in");
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("RefreshToken", refresh, cookieOptions);
        
        return Ok(new { jwt, refresh });
    }
    
    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("RefreshToken", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTime.UtcNow.AddDays(-1)
        });
        
        Logger.Information("Logged out successfully");
        
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command, CancellationToken token)
    {
        Logger.Information($"Assigning {command.Role} role for {command.Email}");
        var result = await Mediator.Send(command, token);
        
        Logger.Information("Role assigned successfully");
        return Ok(result);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(CancellationToken token)
    {
        if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
        {
            return Unauthorized("Refresh token not found");
        }
        Logger.Information("Refreshing jwt token");
        var jwt = await Mediator.Send(new RefreshTokenCommand() { RefreshToken = refreshToken}, token);
        
        Logger.Information("Jwt refreshed successfully");
        return Ok(jwt);
    }
}