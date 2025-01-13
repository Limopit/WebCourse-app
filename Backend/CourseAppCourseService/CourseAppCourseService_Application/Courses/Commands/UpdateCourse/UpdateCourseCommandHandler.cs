using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<UpdateCourseCommand>
{
    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.Courses.GetEntityByIdAsync(request.Id, cancellationToken);

        if (course == null)
        {
            throw new NotFoundException(nameof(Course), request.Id);
        }
        
        await mapper.UpdateAsync(request, course);
        course.UpdateDate = DateTime.UtcNow;
        await unitOfWork.Courses.UpdateAsync(course);
    }
}