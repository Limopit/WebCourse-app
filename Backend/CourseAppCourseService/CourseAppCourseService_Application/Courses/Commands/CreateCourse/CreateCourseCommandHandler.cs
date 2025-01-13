using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<CreateCourseCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await mapper.MapAsync<CreateCourseCommand, Course>(request);
        course.CreationDate = DateTime.UtcNow;
        course.UpdateDate = DateTime.UtcNow;
        
        await unitOfWork.Courses.AddEntityAsync(course, cancellationToken);
        
        return course.Id;
    }
}