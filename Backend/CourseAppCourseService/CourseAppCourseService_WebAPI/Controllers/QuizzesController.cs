using CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;
using CourseAppCourseService_Application.Quizzes.Commands.DeleteQuiz;
using CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;
using CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

public class QuizzesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetQuizList()
    {
        var result = await Mediator.Send(new GetQuizListQuery());
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewQuiz([FromBody] CreateQuizCommand command)
    {
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateQuiz(Guid id, [FromBody]UpdateQuizCommand command)
    {
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuiz(Guid id)
    {
        await Mediator.Send(new DeleteQuizCommand(){ Id = id });
        
        return NoContent();
    }
}