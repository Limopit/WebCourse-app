using CourseAppCourseService_Application.Common.Exceptions;
using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await unitOfWork.Courses.GetCourseByIdWithLessonsAsync(request.Id, cancellationToken);

        if (course == null)
        {
            throw new NotFoundException(nameof(Course), request.Id);
        }
        
        return await mapper.MapAsync<Course, CourseDto>(course);
    }
}