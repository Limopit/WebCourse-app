using CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;

namespace CourseAppCourseService_Tests.Mocks.Quizzes;

public class GetQuizListMock: BaseMock
{
    public GetQuizListQueryHandler Handler { get; private set; }

    public GetQuizListMock()
    {
        Handler = new GetQuizListQueryHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}