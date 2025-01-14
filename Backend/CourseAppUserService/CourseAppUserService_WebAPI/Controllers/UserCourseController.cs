using System.Security.Claims;
using CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Commands.DeleteUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Commands.DeleteUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

public class UserCourseController(IMediator mediator) : BaseController(mediator)
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUserTakenCourse([FromBody] CreateUserTakenCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.Email);
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<Guid>> GetUserTakenCourses()
    {
        var result = await Mediator
            .Send(new GetUsersTakenCoursesQuery { Email = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUserCreatedCourse([FromBody] CreateUserCreatedCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<Guid>> GetUserCreatedCourses(string email)
    {
        var result = await Mediator
            .Send(new GetUserCreatedCoursesQuery { Email = email });
        
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<ActionResult> DeleteUserTakenCourse(string id)
    {
        await Mediator.Send(new DeleteUserTakenCourseCommand() { CourseId = id });

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<ActionResult> DeleteUserCreatedCourse(string id)
    {
        await Mediator.Send(new DeleteUserCreatedCourseCommand() { CourseId = id });

        return Ok();
    }
}