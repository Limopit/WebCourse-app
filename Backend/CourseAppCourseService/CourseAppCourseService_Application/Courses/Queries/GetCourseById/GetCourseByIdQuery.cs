using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseById;

public record GetCourseByIdQuery: IRequest<CourseDto>
{
    public Guid Id { get; init; }
}