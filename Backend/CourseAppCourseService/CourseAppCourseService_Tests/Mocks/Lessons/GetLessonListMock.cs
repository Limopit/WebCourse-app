using CourseAppCourseService_Application.Lessons.Queries.GetLessonList;

namespace CourseAppCourseService_Tests.Mocks.Lessons;

public class GetLessonListMock: BaseMock
{
    public GetLessonListQueryHandler Handler { get; private set; }

    public GetLessonListMock()
    {
        Handler = new GetLessonListQueryHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}