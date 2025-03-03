using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCourseCreator;

namespace CourseAppUserService_Tests.Mocks.UserCreatedCourse;

public class GetUserCourseCreatorMock: BaseMock
{
    public GetUserCourseCreatorQueryHandler Handler { get; private set; }

    public GetUserCourseCreatorMock()
    {
        Handler = new GetUserCourseCreatorQueryHandler(UnitOfWorkMock.Object);
    }
}