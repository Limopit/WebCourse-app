using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Courses.Queries.GetCourseList;

public class GetCourseListQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<GetCourseListQuery, CourseVm>
{
    public async Task<CourseVm> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
    {
        var courses = await unitOfWork.Courses.GetAllEntitiesAsync(cancellationToken);
        
        return await mapper.MapAsync<List<Course>, CourseVm>(courses);
    }
}