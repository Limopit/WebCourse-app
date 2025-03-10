using CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;

namespace CourseAppCourseService_Tests.Mocks.Quizzes;

public class CreateQuizMock : BaseMock
{
    public CreateQuizCommandHandler Handler { get; private set; }

    public CreateQuizMock()
    {
        Handler = new CreateQuizCommandHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}