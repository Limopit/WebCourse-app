using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.UpdateQuiz;

public class UpdateQuizCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<UpdateQuizCommand>
{
    public async Task Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await unitOfWork.Quizzes.GetEntityByIdAsync(request.Id, cancellationToken);
        if (quiz is null)
        {
            throw new NotFoundException(nameof(Quiz), request.Id);
        }
        
        await mapper.UpdateAsync(request, quiz);
        await unitOfWork.Quizzes.UpdateAsync(quiz);
    }
}