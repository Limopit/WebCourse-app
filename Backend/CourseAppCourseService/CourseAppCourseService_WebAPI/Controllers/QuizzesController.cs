using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;
using CourseAppCourseService_Application.Quizzes.Commands.DeleteQuiz;
using CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;
using CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

public class QuizzesController(IMediator mediator, ILoggerService logger) : BaseController(mediator, logger)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetQuizList(CancellationToken cancellationToken)
    {
        Logger.Information("Executing GetQuizList");
        var result = await Mediator.Send(new GetQuizListQuery(), cancellationToken);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewQuiz([FromBody] CreateQuizCommand command, CancellationToken cancellationToken)
    {
        Logger.Information($"Executing CreateNewQuiz with params: {command.Question} | {command.Options} | {command.Answer}");
        var result = await Mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateQuiz(Guid id, [FromBody]UpdateQuizCommand command, CancellationToken cancellationToken)
    {
        Logger.Information($"Executing UpdateQuiz with params: {command.Question} | {command.Options} | {command.Answer}");
        command.Id = id;
        await Mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuiz(Guid id, CancellationToken cancellationToken)
    {
        Logger.Information($"Executing DeleteQuiz with params: {id}");
        await Mediator.Send(new DeleteQuizCommand(){ Id = id }, cancellationToken);
        
        return NoContent();
    }
}