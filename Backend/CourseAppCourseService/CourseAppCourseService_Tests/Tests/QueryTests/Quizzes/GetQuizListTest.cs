using CourseAppCourseService_Application.Quizzes.Queries.GetQuizList;
using CourseAppCourseService_Domain;
using CourseAppCourseService_Tests.Fakers;
using CourseAppCourseService_Tests.Mocks.Quizzes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseAppCourseService_Tests.Tests.QueryTests.Quizzes;

public class GetQuizListTest(GetQuizListMock mock): IClassFixture<GetQuizListMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnCourseVm_WhenCoursesExist()
    {
        // Arrange
        var quizzes = _fakers.QuizFaker.Generate(3);

        var quizVm = new QuizVm()
        {
            Quizzes = quizzes.Select(c => new Quiz
            {
                Question = c.Question,
                Answer = c.Answer,
                Options = c.Options
            }).ToList()
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Quizzes.GetAllEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(quizzes);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<Quiz>, QuizVm>(quizzes))
            .ReturnsAsync(quizVm);

        var query = new GetQuizListQuery();

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Quizzes.Should().HaveCount(3);
        result.Quizzes.Should().BeEquivalentTo(quizVm.Quizzes);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyCourseVm_WhenNoCoursesExist()
    {
        // Arrange
        var quizzes = new List<Quiz>();

        var quizVm = new QuizVm()
        {
            Quizzes = new List<Quiz>()
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Quizzes.GetAllEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(quizzes);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<Quiz>, QuizVm>(quizzes))
            .ReturnsAsync(quizVm);

        var query = new GetQuizListQuery();

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Quizzes.Should().BeEmpty();
    }
}