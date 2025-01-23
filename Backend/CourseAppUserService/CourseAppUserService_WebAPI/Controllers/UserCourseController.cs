using System.Security.Claims;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[Route("api/Users")]
public class UserCourseController(IMediator mediator, ILoggerService logger) : BaseController(mediator, logger)
{
    [Authorize]
    [HttpPost("courses/taken")]
    public async Task<ActionResult<Guid>> CreateUserTakenCourse([FromBody] CreateUserTakenCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.Email);
        
        var result = await Mediator.Send(command);
        
        Logger.Information($"User {command.Email} takes the {command.CourseId} course");
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("courses/taken")]
    public async Task<ActionResult<Guid>> GetUserTakenCourses()
    {
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await Mediator
            .Send(new GetUsersTakenCoursesQuery { Email = email });
        
        Logger.Information($"User {email} got the taken course list");
        return Ok(result);
    }
    
    [HttpGet("{email}/courses/created")]
    public async Task<ActionResult<Guid>> GetUserCreatedCourses(string email)
    {
        var result = await Mediator
            .Send(new GetUserCreatedCoursesQuery { Email = email });
        
        Logger.Information($"Executed listing {email} created courses");
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{email}/courses/taken/{id}")]
    public async Task<ActionResult> DeleteUserTakenCourse(string id, string email)
    {
        await Mediator.Send(new DeleteUserTakenCourseCommand() { Id = id, Email = email });
        
        Logger.Information($"User`s ({email}) taken ({id}) course was deleted");
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("courses/created/{id}")]
    public async Task<ActionResult> DeleteUserCreatedCourse(string id)
    {
        await Mediator.Send(new DeleteUserCreatedCourseCommand() { Id = id });

        Logger.Information($"{id} course was deleted");
        return NoContent();
    }
}