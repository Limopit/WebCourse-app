using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseListInfo;

public class GetCourseListInfoQuery: IRequest<CourseVm>
{
    public IList<string> CourseIds { get; set; }
}