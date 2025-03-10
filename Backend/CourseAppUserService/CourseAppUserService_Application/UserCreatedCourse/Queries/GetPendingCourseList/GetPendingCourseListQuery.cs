using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetPendingCourseList;

public record GetPendingCourseListQuery: IRequest<IList<string>>;