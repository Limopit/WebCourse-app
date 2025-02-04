using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.DeleteUser;
using CourseAppUserService_Application.Users.Commands.UpdateUserData;
using CourseAppUserService_Application.Users.Commands.UpdateUserPassword;
using CourseAppUserService_Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

public class UsersController(IMediator mediator, ILoggerService logger) : BaseController(mediator, logger)
{
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDataCommand command)
    {
        await Mediator.Send(command);
        
        Logger.Information("User info was updated successfully");
        return Ok();
    }
    
    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordCommand command)
    {
        await Mediator.Send(command);
        
        Logger.Information("User password was updated successfully");
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        var result = await Mediator.Send(new GetUserCommand{Email = email});
        
        Logger.Information("User info was retrieved successfully");
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        await Mediator.Send(new DeleteUserCommand{Email = email});
        
        Logger.Information("User was deleted successfully");
        return Ok();
    }
}