using CourseAppUserService_Application.Interfaces;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetApprovedCourseList;

public class GetApprovedCourseListQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetApprovedCourseListQuery, IList<string>>
{
    public async Task<IList<string>> Handle(GetApprovedCourseListQuery request, CancellationToken cancellationToken)
    {
        var courses = await unitOfWork.UserCreatedCourses.GetUserApprovedCoursesAsync(cancellationToken);
        
        return courses;
    }
}