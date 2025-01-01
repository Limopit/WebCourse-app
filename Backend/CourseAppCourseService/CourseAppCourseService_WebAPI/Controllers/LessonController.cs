using CourseAppCourseService_Application.Lessons.Commands;
using CourseAppCourseService_Application.Lessons.Queries.GetLessonList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

public class LessonController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetLessonList()
    {
        var result = await Mediator.Send(new GetLessonListQuery());
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewLesson([FromBody] CreateLessonCommand command)
    {
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
}