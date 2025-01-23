using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Application.Lessons.Commands.CreateLesson;
using CourseAppCourseService_Application.Lessons.Commands.DeleteLesson;
using CourseAppCourseService_Application.Lessons.Commands.UpdateLesson;
using CourseAppCourseService_Application.Lessons.Queries.GetLessonList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

public class LessonsController(IMediator mediator, ILoggerService logger) : BaseController(mediator, logger)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetLessonList()
    {
        Logger.Information("Executing GetLessonList");
        var result = await Mediator.Send(new GetLessonListQuery());
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewLesson([FromBody] CreateLessonCommand command)
    {
        Logger.Information($"Executing CreateNewLesson with params: {command.Title} | {command.Description} | {command.Content}");
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLesson(Guid id, [FromBody]UpdateLessonCommand command)
    {
        Logger.Information($"Executing UpdateLesson with params: {command.Title} | {command.Description} | {command.Content}");
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        Logger.Information($"Executing DeleteLesson with params: {id}");
        await Mediator.Send(new DeleteLessonCommand(){ Id = id });
        
        return NoContent();
    }
}