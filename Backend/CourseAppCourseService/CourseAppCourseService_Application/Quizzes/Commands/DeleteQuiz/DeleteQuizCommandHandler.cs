using AutoMapper;
using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteQuizCommand>
{
    public async Task Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await unitOfWork.Quizzes.GetEntityByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
        {
            throw new NotFoundException(nameof(Quiz), request.QuizId);
        }
        
        await unitOfWork.Quizzes.RemoveEntityAsync(quiz);
    }
}