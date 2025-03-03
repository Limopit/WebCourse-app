using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.UserCreatedCourse.Queries.GetUserCourseCreator;
using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Domain.Enums;
using CourseAppUserService_Tests.Mocks.UserCreatedCourse;
using FluentAssertions;
using Moq;

namespace CourseAppUserService_Tests.Tests.Users.QueryTests;

public class GetUserCourseCreatorTests(GetUserCourseCreatorMock mock) : IClassFixture<GetUserCourseCreatorMock>
{
    [Fact]
    public async Task Handle_ShouldReturnUserEmail_WhenCourseAndUserExist()
    {
        // Arrange
        var courseId = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid().ToString();
        var userEmail = "creator@example.com";

        var userCreatedCourse = new UserCreatedCourses
        {
            CourseId = courseId,
            UserId = userId,
            ApprovementStatus = ApprovementStatus.Pending.ToString(),
        };

        var user = new User
        {
            Id = userId,
            Email = userEmail
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.UserCreatedCourses.GetUserCreatedCourseByCourseIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userCreatedCourse);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByIdAsync(userId))
            .ReturnsAsync(user);

        var query = new GetUserCourseCreatorQuery { CourseId = courseId };

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().Be(userEmail);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCourseDoesNotExist()
    {
        // Arrange
        var courseId = Guid.NewGuid().ToString();

        mock.UnitOfWorkMock
            .Setup(uow => uow.UserCreatedCourses.GetUserCreatedCourseByCourseIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserCreatedCourses)null);

        var query = new GetUserCourseCreatorQuery { CourseId = courseId };

        // Act
        Func<Task> act = async () => await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Entity \"UserCreatedCourse\" ({courseId}) not found");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var courseId = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid().ToString();

        var userCreatedCourse = new UserCreatedCourses
        {
            CourseId = courseId,
            UserId = userId,
            ApprovementStatus = ApprovementStatus.Pending.ToString(),
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.UserCreatedCourses.GetUserCreatedCourseByCourseIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userCreatedCourse);

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByIdAsync(userId))
            .ReturnsAsync((User)null);

        var query = new GetUserCourseCreatorQuery { CourseId = courseId };

        // Act
        Func<Task> act = async () => await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Entity \"UserCreatedCourse\" ({courseId}) not found");
    }
}