using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetApprovedCourseList;

public record GetApprovedCourseListQuery: IRequest<IList<string>>;