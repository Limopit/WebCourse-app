using CourseAppCourseService_Application.Courses.Commands.CreateCourse;
using CourseAppCourseService_Domain;
using CourseAppCourseService_Tests.Fakers;
using CourseAppCourseService_Tests.Mocks.Courses;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseAppCourseService_Tests.Tests.CommandTests.Courses;

public class CreateCourseTests(CreateCourseMock mock) : IClassFixture<CreateCourseMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnCourseId_WhenCourseIsCreated()
    {
        // Arrange
        var command = new CreateCourseCommand();

        var course = _fakers.CourseFaker
            .RuleFor(c => c.Title, command.Title)
            .RuleFor(c => c.Description, command.Description)
            .Generate();

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateCourseCommand, Course>(command))
            .ReturnsAsync(course);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Courses.AddEntityAsync(course, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(course.Id);
        mock.UnitOfWorkMock.Verify(uow => uow.Courses.AddEntityAsync(course, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateCourseWithCorrectFields_WhenCourseIsCreated()
    {
        // Arrange
        var command = new CreateCourseCommand();

        var course = _fakers.CourseFaker
            .RuleFor(c => c.Title, command.Title)
            .RuleFor(c => c.Description, command.Description)
            .Generate();

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateCourseCommand, Course>(command))
            .ReturnsAsync(course);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Courses.AddEntityAsync(course, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        course.Id.Should().Be(result);
        course.Title.Should().Be(command.Title);
        course.Description.Should().Be(command.Description);
        course.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        course.UpdateDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));

        mock.UnitOfWorkMock.Verify(
            uow => uow.Courses.AddEntityAsync(course, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}