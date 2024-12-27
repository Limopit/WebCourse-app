using CourseAppUserService_Application.Users.Commands.AssignRole;
using CourseAppUserService_Application.Users.Commands.DeleteUser;
using CourseAppUserService_Application.Users.Commands.LoginUser;
using CourseAppUserService_Application.Users.Commands.RefreshToken;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using CourseAppUserService_Application.Users.Commands.UpdateUserData;
using CourseAppUserService_Application.Users.Commands.UpdateUserPassword;
using CourseAppUserService_Application.Users.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[Route("api/[controller]")]
public class UserController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var userId = await Mediator.Send(command);
        return Ok(new { UserId = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var (jwt, refresh) = await Mediator.Send(command);
        return Ok(new { jwt, refresh });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var jwt = await Mediator.Send(command); 
        return Ok(jwt);
    }
    
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataCommand command)
    {
        await Mediator.Send(command); 
        return Ok();
    }
    
    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordCommand command)
    {
        await Mediator.Send(command); 
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("user-data")]
    public async Task<IActionResult> GetUserData(string email)
    {
        var result = await Mediator.Send(new GetUserInfoCommand{Email = email});
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string email)
    {
        await Mediator.Send(new DeleteUserCommand{Email = email});
        return Ok();
    }
}