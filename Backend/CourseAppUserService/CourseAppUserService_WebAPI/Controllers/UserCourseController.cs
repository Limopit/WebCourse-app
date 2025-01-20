using System.Security.Claims;
using CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteEachUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

public class UserCourseController(IMediator mediator) : BaseController(mediator)
{
    [Authorize]
    [HttpPost("taken-course/courses")]
    public async Task<ActionResult<Guid>> CreateUserTakenCourse([FromBody] CreateUserTakenCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.Email);
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("taken-course/courses")]
    public async Task<ActionResult<Guid>> GetUserTakenCourses()
    {
        var result = await Mediator
            .Send(new GetUsersTakenCoursesQuery { Email = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("user-course/courses")]
    public async Task<ActionResult<Guid>> CreateUserCreatedCourse([FromBody] CreateUserCreatedCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpGet("{email}/user-course/courses")]
    public async Task<ActionResult<Guid>> GetUserCreatedCourses(string email)
    {
        var result = await Mediator
            .Send(new GetUserCreatedCoursesQuery { Email = email });
        
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("users/{email}/taken-course/{id}")]
    public async Task<ActionResult> DeleteUserTakenCourse(string id, string email)
    {
        await Mediator.Send(new DeleteUserTakenCourseCommand() { CourseId = id, Email = email });

        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("user-course/{id}")]
    public async Task<ActionResult> DeleteUserCreatedCourse(string id)
    {
        await Mediator.Send(new DeleteUserCreatedCourseCommand() { CourseId = id });

        return NoContent();
    }
}