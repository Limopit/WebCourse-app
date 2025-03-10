using CourseAppCourseService_Application.Courses.Commands.CreateCourse;

namespace CourseAppCourseService_Tests.Mocks.Courses;

public class CreateCourseMock : BaseMock
{
    public CreateCourseCommandHandler Handler { get; private set; }

    public CreateCourseMock()
    {
        Handler = new CreateCourseCommandHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}