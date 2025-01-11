using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using CourseAppCourseService_Domain;
using MediatR;

namespace CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

public class GetLessonListQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper): IRequestHandler<GetLessonListQuery, LessonVm>
{
    public async Task<LessonVm> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
    {
        var lessons = await unitOfWork.Lessons.GetAllEntitiesAsync(cancellationToken);
        
        return await mapper.MapAsync<List<Lesson>, LessonVm>(lessons);
    }
}