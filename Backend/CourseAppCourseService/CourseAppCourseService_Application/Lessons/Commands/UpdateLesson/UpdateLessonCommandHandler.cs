using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands.UpdateLesson;

public class UpdateLessonCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<UpdateLessonCommand>
{
    public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await unitOfWork.Lessons.GetEntityByIdAsync(request.Id, cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException(nameof(Lesson), request.Id);
        }
        
        await mapper.UpdateAsync(request, lesson);
        await unitOfWork.Lessons.UpdateAsync(lesson);
    }
}