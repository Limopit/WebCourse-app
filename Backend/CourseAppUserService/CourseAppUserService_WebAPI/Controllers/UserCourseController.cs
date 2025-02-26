using System.Security.Claims;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Commands.SetUserCourseApprovementStatus;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using CourseAppUserService_Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[Route("api/Users")]
public class UserCourseController(IMediator mediator, ILoggerService logger) : BaseController(mediator, logger)
{
    [Authorize]
    [HttpPost("courses/taken")]
    public async Task<ActionResult<Guid>> CreateUserTakenCourse([FromBody] CreateUserTakenCourseCommand command, CancellationToken cancellationToken)
    {
        Logger.Information($"User {command.Email} takes the {command.CourseId} course");
        
        var result = await Mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("courses/taken")]
    public async Task<ActionResult<Guid>> GetUserTakenCourses(CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await Mediator
            .Send(new GetUsersTakenCoursesQuery { Email = email }, cancellationToken);
        
        Logger.Information($"User {email} got the taken course list");
        return Ok(result);
    }
    
    [HttpGet("{email}/courses/created")]
    public async Task<ActionResult<Guid>> GetUserCreatedCourses(string email, CancellationToken cancellationToken)
    {
        var result = await Mediator
            .Send(new GetUserCreatedCoursesQuery { Email = email }, cancellationToken);
        
        Logger.Information($"Executed listing {email} created courses");
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{email}/courses/taken/{id}")]
    public async Task<ActionResult> DeleteUserTakenCourse(string id, string email, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteUserTakenCourseCommand() { Id = id, Email = email }, cancellationToken);
        
        Logger.Information($"User`s ({email}) taken ({id}) course was deleted");
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("courses/created/{id}")]
    public async Task<ActionResult> DeleteUserCreatedCourse(string id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteUserCreatedCourseCommand() { Id = id }, cancellationToken);

        Logger.Information($"{id} course was deleted");
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("courses/created/{id}")]
    public async Task<ActionResult> SetApprovementStatus(string id, ApprovementStatus status, CancellationToken cancellationToken)
    {
        await Mediator.Send(new SetUserCourseApprovementStatusCommand{ CourseId = id, Status = status }, cancellationToken);
        
        return NoContent();
    }
}