using CourseAppCourseService_Application.Courses.Queries.GetCourseList;

namespace CourseAppCourseService_Tests.Mocks.Courses;

public class GetCourseListMock: BaseMock
{
    public GetCourseListQueryHandler Handler { get; private set; }

    public GetCourseListMock()
    {
        Handler = new GetCourseListQueryHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}