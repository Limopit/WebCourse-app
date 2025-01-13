using CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;
using CourseAppCourseService_Application.Quizzes.Commands.DeleteQuiz;
using CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;
using CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppCourseService.Controllers;

[Route("api/[controller]/[action]")]
public class QuizController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<Guid>> GetQuizList()
    {
        var result = await Mediator.Send(new GetQuizListQuery());
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewQuiz([FromBody] CreateQuizCommand command)
    {
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateQuiz([FromBody]UpdateQuizCommand command)
    {
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteQuiz(Guid id)
    {
        await Mediator.Send(new DeleteQuizCommand(){ QuizId = id });
        
        return NoContent();
    }
}