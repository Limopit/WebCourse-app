using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseListInfo;

public class GetCourseListInfoQuery: IRequest<CourseVm>
{
    public IList<string> CourseIds { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}