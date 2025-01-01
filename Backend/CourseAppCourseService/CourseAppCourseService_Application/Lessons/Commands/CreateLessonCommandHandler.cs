using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Lessons.Commands;

public class CreateLessonCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<CreateLessonCommand, Guid>
{
    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await mapper.MapAsync<CreateLessonCommand, Lesson>(request);
        
        await unitOfWork.Lessons.AddEntityAsync(lesson, cancellationToken);
        
        return lesson.Id;
    }
}