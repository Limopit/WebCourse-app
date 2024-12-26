using System.Security.Claims;
using CourseAppUserService_Application.UserCreatedCourse.Commands.CreateUserCreatedCourse;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCreatedCourses;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppUserService.Controllers;

[Route("api/[controller]")]
public class UserCourseController(IMediator mediator) : BaseController(mediator)
{
    [Authorize]
    [HttpPost("user/courses/taken/new")]
    public async Task<ActionResult<Guid>> CreateUserTakenCourse([FromBody] CreateUserTakenCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("user/courses/taken/get")]
    public async Task<ActionResult<Guid>> GetUserTakenCourses()
    {
        var result = await Mediator
            .Send(new GetUsersTakenCoursesQuery { Email = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("user/courses/created/new")]
    public async Task<ActionResult<Guid>> CreateUserCreatedCourse([FromBody] CreateUserCreatedCourseCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpGet("user/courses/created/get")]
    public async Task<ActionResult<Guid>> GetUserCreatedCourses(string email)
    {
        var result = await Mediator
            .Send(new GetUserCreatedCoursesQuery { Email = email });
        
        return Ok(result);
    }
}