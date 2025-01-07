using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
using CourseAppCourseService_Application.Courses.Commands.DeleteCourse;
using CourseAppCourseService_Application.Courses.Commands.UpdateCourse;
using CourseAppCourseService_Application.Courses.Queries.GetCourseList;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserServiceRpc;

namespace CourseAppCourseService.Controllers;

[Route("api/[controller]/[action]")]
public class CourseController(IMediator mediator, UserServiceRpc.UserService.UserServiceClient userServiceClient) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetCourseList()
    {
        var result = await Mediator.Send(new GetCourseListQuery());
        
        return Ok(result);
    }
    
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
    
    [HttpPut]
    public async Task<ActionResult> UpdateCourse([FromBody]UpdateCourseCommand command)
    {
        await Mediator.Send(command);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteCourseCommand(){ Id = id });

            var grpcRequest = new DeleteCourseRecordRequest()
            {
                CourseId = id.ToString()
            };
            
            userServiceClient.DeleteCourseRecords(grpcRequest);
            
            return Ok();
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
}