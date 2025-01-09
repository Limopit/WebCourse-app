using CourseAppCourseService_Application.Lessons.Commands.CreateLesson;
using CourseAppCourseService_Application.Lessons.Commands.DeleteLesson;
using CourseAppCourseService_Application.Lessons.Commands.UpdateLesson;
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
    
    [HttpPut]
    public async Task<ActionResult> UpdateLesson([FromBody]UpdateLessonCommand command)
    {
        await Mediator.Send(command);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        await Mediator.Send(new DeleteLessonCommand(){ LessonId = id });
        
        return Ok();
    }
}