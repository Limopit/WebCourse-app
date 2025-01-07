using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteCourseCommand>
{
    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.Courses.GetEntityByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            throw new NotFoundException(nameof(Course), request.Id);
        }
        
        await unitOfWork.Courses.RemoveEntityAsync(course);
    }
}