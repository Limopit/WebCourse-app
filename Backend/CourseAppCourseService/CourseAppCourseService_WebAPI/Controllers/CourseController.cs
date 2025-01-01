using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
using CourseAppCourseService_Application.Courses.Queries.GetCourseList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

[Route("api/[controller]/[action]")]
public class CourseController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<ActionResult> CreateCourse([FromBody]CreateCourseCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<Guid>> GetCourseList()
    {
        var result = await Mediator.Send(new GetCourseListQuery());
        
        return Ok(result);
    }
}