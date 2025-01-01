using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<CreateQuizCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await mapper.MapAsync<CreateQuizCommand, Quiz>(request);
        
        await unitOfWork.Quizzes.AddEntityAsync(quiz, cancellationToken);
        
        return quiz.Id;
    }
}