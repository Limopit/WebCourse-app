using CourseAppCourseService_Application.Lessons.Commands.CreateLesson;
using CourseAppCourseService_Application.Lessons.Commands.DeleteLesson;
using CourseAppCourseService_Application.Lessons.Commands.UpdateLesson;
using CourseAppCourseService_Application.Lessons.Queries.GetLessonList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

public class LessonController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("lesson-list")]
    public async Task<ActionResult<Guid>> GetLessonList()
    {
        var result = await Mediator.Send(new GetLessonListQuery());
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("new")]
    public async Task<ActionResult<Guid>> CreateNewLesson([FromBody] CreateLessonCommand command)
    {
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLesson(Guid id, [FromBody]UpdateLessonCommand command)
    {
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        await Mediator.Send(new DeleteLessonCommand(){ LessonId = id });
        
        return NoContent();
    }
}