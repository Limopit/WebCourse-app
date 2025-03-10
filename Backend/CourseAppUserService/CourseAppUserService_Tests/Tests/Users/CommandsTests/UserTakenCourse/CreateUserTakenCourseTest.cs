using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Tests.Fakers;
using CourseAppUserService_Tests.Mocks.UserTakenCourse;
using Usr = CourseAppUserService_Domain.Entities.User;
using FluentAssertions;
using Moq;

namespace CourseAppUserService_Tests.Tests.Users.CommandsTests.UserTakenCourse;

public class CreateUserTakenCourseTest(CreateUserTakenCourseMock mock) : IClassFixture<CreateUserTakenCourseMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnRecordId_WhenUserAndCourseAreValid()
    {
        // Arrange
        var email = "testuser@example.com";
        var userId = Guid.NewGuid().ToString();
        var recordId = Guid.NewGuid();

        var user = _fakers.Users
            .RuleFor(u => u.Id, userId)
            .RuleFor(u => u.Email, email)
            .Generate();

        var command = new CreateUserTakenCourseCommand
        {
            Email = email,
            CourseId = Guid.NewGuid().ToString(),
            StartDate = DateTime.UtcNow
        };

        var userTakenCourse = _fakers.UserTakenCourses
            .RuleFor(c => c.RecordId, recordId)
            .RuleFor(c => c.UserId, userId)
            .RuleFor(c => c.CourseId, command.CourseId)
            .RuleFor(c => c.DateStart, command.StartDate)
            .Generate();

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByEmailAsync(email))
            .ReturnsAsync(user);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<CreateUserTakenCourseCommand, UserTakenCourses>(command))
            .ReturnsAsync(userTakenCourse);

        mock.UnitOfWorkMock
            .Setup(uow => uow.UserTakenCourses.AddEntityAsync(userTakenCourse, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        mock.UnitOfWorkMock
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(recordId);
        mock.UnitOfWorkMock.Verify(uow => uow.UserTakenCourses.AddEntityAsync(userTakenCourse, It.IsAny<CancellationToken>()), Times.Once);
        mock.UnitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var email = "nonexistentuser@example.com";

        var command = new CreateUserTakenCourseCommand
        {
            Email = email,
            CourseId = Guid.NewGuid().ToString(),
            StartDate = DateTime.UtcNow
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByEmailAsync(email))
            .ReturnsAsync((Usr)null);

        // Act
        Func<Task> act = async () => await mock.Handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Entity \"User\" ({email}) not found");
    }
}