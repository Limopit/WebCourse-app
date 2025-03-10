using CourseAppCourseService_Application.Lessons.Commands.CreateLesson;

namespace CourseAppCourseService_Tests.Mocks.Lessons;

public class CreateLessonMock : BaseMock
{
    public CreateLessonCommandHandler Handler { get; private set; }

    public CreateLessonMock()
    {
        Handler = new CreateLessonCommandHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}