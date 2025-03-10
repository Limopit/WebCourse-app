using CourseAppCourseService_Application.Quizzes.Commands.CreateQuiz;
using CourseAppCourseService_Domain;
using CourseAppCourseService_Tests.Fakers;
using CourseAppCourseService_Tests.Mocks.Quizzes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseAppCourseService_Tests.Tests.CommandTests.Quizzes;

public class CreateQuizTest(CreateQuizMock mock): IClassFixture<CreateQuizMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnLessonId_WhenLessonIsCreated()
    {
        // Arrange
        var command = new CreateQuizCommand();

        var quiz = _fakers.QuizFaker
            .RuleFor(c => c.Question, command.Question)
            .RuleFor(c => c.Answer, command.Answer)
            .RuleFor(c => c.Options, command.Options)
            .Generate();

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateQuizCommand, Quiz>(command))
            .ReturnsAsync(quiz);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Quizzes.AddEntityAsync(quiz, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(quiz.Id);
        mock.UnitOfWorkMock.Verify(uow => uow.Quizzes.AddEntityAsync(quiz, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateLessonWithCorrectFields_WhenLessonIsCreated()
    {
        // Arrange
        var command = new CreateQuizCommand();

        var quiz = _fakers.QuizFaker
            .RuleFor(c => c.Question, command.Question)
            .RuleFor(c => c.Answer, command.Answer)
            .RuleFor(c => c.Options, command.Options)
            .Generate();

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateQuizCommand, Quiz>(command))
            .ReturnsAsync(quiz);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Quizzes.AddEntityAsync(quiz, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        quiz.Id.Should().Be(result);
        quiz.Question.Should().Be(command.Question);
        quiz.Answer.Should().Be(command.Answer);
        
        mock.UnitOfWorkMock.Verify(
            uow => uow.Quizzes.AddEntityAsync(quiz, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}