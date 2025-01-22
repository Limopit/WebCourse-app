using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public record GetCourseListQuery: IRequest<CourseVm>;