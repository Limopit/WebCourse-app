using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
using CourseAppCourseService_Application.Courses.Queries.GetCourseList;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserServiceRpc;

namespace CourseAppCourseService.Controllers;

[Route("api/[controller]/[action]")]
public class CourseController(IMediator mediator, UserServiceRpc.UserService.UserServiceClient userServiceClient) : BaseController(mediator)
{
    [HttpPost]
    public async Task<ActionResult> CreateCourse([FromBody]CreateCourseCommand command)
    {
        try
        {
            var createdRecordId = await mediator.Send(command);

            var grpcRequest = new CreateCourseRequest()
            {
                CourseId = createdRecordId.ToString()
            };

            userServiceClient.CreateUserCreatedCourseRecord(grpcRequest);

            return Ok(new { RecordId = createdRecordId });
        }
        catch (RpcException grpcEx)
        {
            return StatusCode((int)grpcEx.StatusCode, grpcEx.Status.Detail);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<Guid>> GetCourseList()
    {
        var result = await Mediator.Send(new GetCourseListQuery());
        
        return Ok(result);
    }
}