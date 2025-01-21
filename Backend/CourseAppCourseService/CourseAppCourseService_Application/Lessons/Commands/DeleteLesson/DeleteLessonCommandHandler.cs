using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands.DeleteLesson;

public class DeleteLessonCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteLessonCommand>
{
    public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await unitOfWork.Lessons.GetEntityByIdAsync(request.Id, cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException(nameof(Lesson), request.Id);
        }

        await unitOfWork.Lessons.RemoveEntityAsync(lesson);
    }
}