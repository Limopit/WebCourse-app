using System.Security.Claims;
using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
using CourseAppCourseService_Application.Courses.Commands.DeleteCourse;
using CourseAppCourseService_Application.Courses.Commands.UpdateCourse;
using CourseAppCourseService_Application.Courses.Queries.GetCourseById;
using CourseAppCourseService_Application.Courses.Queries.GetCourseList;
using CourseAppCourseService_Application.Courses.Queries.GetCourseListInfo;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Infrastructure.Services.UserService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

public class CoursesController(IMediator mediator, ILoggerService logger, GrpcUserServiceClient userServiceClient) : BaseController(mediator, logger)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetCourseList(CancellationToken cancellationToken)
    {
        Logger.Information("Executing GetCourseList");
        var result = await Mediator.Send(new GetCourseListQuery(), cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Guid>> GetCourseWithLessons(Guid id, CancellationToken cancellationToken)
    {
        Logger.Information("Executing GetCourseWithLessons");
        var result = await Mediator.Send(new GetCourseByIdQuery() { Id = id }, cancellationToken);
        
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateCourse([FromBody] CreateCourseCommand command, CancellationToken cancellationToken)
    {
        Logger.Information($"Executing CreateCourse with params: {command.Title} | {command.Description} | {command.Logo}");
        var createdRecordId = await mediator.Send(command, cancellationToken);

        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Logger.Information($"Executing gRPC CreateUserCreatedCourseRecord request to UserService with params: {email} | {createdRecordId}");
        userServiceClient.CreateUserCreatedCourseRecord(email, createdRecordId.ToString());

        return Ok(new { RecordId = createdRecordId });
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(Guid id, [FromBody]UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        Logger.Information($"Executing UpdateCourse with params: {id} | {command.Title} | {command.Description} | {command.Logo}");
        command.Id = id;
        await Mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(Guid id, CancellationToken cancellationToken)
    {
        Logger.Information($"Executing DeleteCourse with params: {id}");
        await Mediator.Send(new DeleteCourseCommand() { Id = id }, cancellationToken);

        Logger.Information($"Executing gRPC DeleteUserCourseRecord request to UserService with params: {id}");
        var result = userServiceClient.DeleteUserCourseRecord(id.ToString());

        return NoContent();

    }
    
    [HttpGet("approved")]
    public async Task<ActionResult<Guid>> GetApprovedCourseList(CancellationToken cancellationToken)
    {
        Logger.Information("Getting Approved Course List");
        var approvedCourseList = userServiceClient.GetApprovedCourseList();
        
        Logger.Information("Getting Approved Courses info");
        var result = await Mediator.Send(new GetCourseListInfoQuery() { CourseIds = approvedCourseList }, cancellationToken);

        return Ok(result);

    }
}