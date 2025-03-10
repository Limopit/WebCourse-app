using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseListInfo;

public class GetCourseListInfoQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<GetCourseListInfoQuery, CourseVm>
{
    public async Task<CourseVm> Handle(GetCourseListInfoQuery request, CancellationToken cancellationToken)
    {
        List<Guid> guidList = request.CourseIds.Select(Guid.Parse).ToList();
        var courses = await unitOfWork.Courses.GetEntityListInfoByIdAsync(guidList, request.PageNumber, request.PageSize, cancellationToken);
        
        return await mapper.MapAsync<List<Course>, CourseVm>(courses);
    }
}