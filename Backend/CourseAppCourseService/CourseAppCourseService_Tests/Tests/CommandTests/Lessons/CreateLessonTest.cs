using CourseAppCourseService_Application.Lessons.Commands.CreateLesson;
using CourseAppCourseService_Domain;
using CourseAppCourseService_Tests.Fakers;
using CourseAppCourseService_Tests.Mocks.Lessons;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseAppCourseService_Tests.Tests.CommandTests.Lessons;

public class CreateLessonTest(CreateLessonMock mock) : IClassFixture<CreateLessonMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnLessonId_WhenLessonIsCreated()
    {
        // Arrange
        var command = new CreateLessonCommand();

        var lesson = _fakers.LessonFaker
            .RuleFor(c => c.Title, command.Title)
            .RuleFor(c => c.Description, command.Description)
            .Generate();

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateLessonCommand, Lesson>(command))
            .ReturnsAsync(lesson);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Lessons.AddEntityAsync(lesson, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(lesson.Id);
        mock.UnitOfWorkMock.Verify(uow => uow.Lessons.AddEntityAsync(lesson, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateLessonWithCorrectFields_WhenLessonIsCreated()
    {
        // Arrange
        var command = new CreateLessonCommand();

        var lesson = _fakers.LessonFaker
            .RuleFor(c => c.Title, command.Title)
            .RuleFor(c => c.Description, command.Description)
            .RuleFor(c => c.Duration, command.Duration)
            .Generate();

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateLessonCommand, Lesson>(command))
            .ReturnsAsync(lesson);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Lessons.AddEntityAsync(lesson, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        lesson.Id.Should().Be(result);
        lesson.Title.Should().Be(command.Title);
        lesson.Description.Should().Be(command.Description);
        lesson.Duration.Should().Be(command.Duration);

        mock.UnitOfWorkMock.Verify(
            uow => uow.Lessons.AddEntityAsync(lesson, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}