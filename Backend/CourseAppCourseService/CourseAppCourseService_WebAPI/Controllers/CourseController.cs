using System.Security.Claims;
using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
using CourseAppCourseService_Application.Courses.Commands.DeleteCourse;
using CourseAppCourseService_Application.Courses.Commands.UpdateCourse;
using CourseAppCourseService_Application.Courses.Queries.GetCourseList;
using CourseAppCourseService_Infrastructure.Services.UserService;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceRpc;

namespace CourseAppCourseService.Controllers;

public class CourseController(IMediator mediator, GrpcUserServiceClient userServiceClient) : BaseController(mediator)
{
    [HttpGet("course-list")]
    public async Task<ActionResult<Guid>> GetCourseList()
    {
        var result = await Mediator.Send(new GetCourseListQuery());
        
        return Ok(result);
    }

    [Authorize]
    [HttpPost("new")]
    public async Task<ActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var createdRecordId = await mediator.Send(command);
        
        userServiceClient.CreateUserCreatedCourseRecord(User.FindFirstValue(ClaimTypes.NameIdentifier), createdRecordId.ToString());

        return Ok(new { RecordId = createdRecordId });
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(Guid id, [FromBody]UpdateCourseCommand command)
    {
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        await Mediator.Send(new DeleteCourseCommand() { Id = id });

        var result = userServiceClient.DeleteUserCourseRecord(id.ToString());

        return NoContent();

    }
}