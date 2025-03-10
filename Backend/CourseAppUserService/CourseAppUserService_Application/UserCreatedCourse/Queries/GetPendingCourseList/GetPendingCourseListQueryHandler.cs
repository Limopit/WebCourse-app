using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Domain.Enums;
using MediatR;

namespace CourseAppUserService_Application.UserCreatedCourse.Queries.GetPendingCourseList;

public class GetPendingCourseListQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetPendingCourseListQuery, IList<string>>
{
    public async Task<IList<string>> Handle(GetPendingCourseListQuery request, CancellationToken cancellationToken)
    {
        var courses = await unitOfWork.UserCreatedCourses.GetUserCoursesAsync(ApprovementStatus.Pending, cancellationToken);
        
        return courses;
    }
}