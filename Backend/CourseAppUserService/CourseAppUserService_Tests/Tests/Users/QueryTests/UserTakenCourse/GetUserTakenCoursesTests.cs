using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Tests.Fakers;
using CourseAppUserService_Tests.Mocks.UserTakenCourse;
using FluentAssertions;
using Moq;

namespace CourseAppUserService_Tests.Tests.Users.QueryTests.UserTakenCourse;

public class GetUsersTakenCoursesTests(GetUsersTakenCoursesMock mock) : IClassFixture<GetUsersTakenCoursesMock>
{
    private readonly FakerContext _fakers = new FakerContext();

    [Fact]
    public async Task Handle_ShouldReturnUserTakenCourseVm_WhenUserAndCoursesExist()
    {
        // Arrange
        var email = "testuser@example.com";
        var userId = Guid.NewGuid().ToString();

        var user = _fakers.Users
            .RuleFor(u => u.Id, userId)
            .RuleFor(u => u.Email, email)
            .Generate();

        var courses = _fakers.UserTakenCourses
            .RuleFor(c => c.UserId, userId)
            .Generate(2);

        var mappedCourses = courses
            .Select(course => _fakers.UserTakenCourseDtos
                .RuleFor(dto => dto.Id, course.CourseId)
                .RuleFor(dto => dto.Status, course.Status)
                .RuleFor(dto => dto.StartDate, (DateTime)course.DateStart)
                .Generate())
            .ToList();

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByEmailAsync(email))
            .ReturnsAsync(user);

        mock.UnitOfWorkMock
            .Setup(uow => uow.UserTakenCourses.GetUserTakenCoursesAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<UserTakenCourses>, IList<UserTakenCourseDto>>(courses))
            .ReturnsAsync(mappedCourses);

        var query = new GetUsersTakenCoursesQuery { Email = email };

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserTakenCourses.Should().HaveCount(2);
        result.UserTakenCourses.Should().BeEquivalentTo(mappedCourses);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var email = "nonexistentuser@example.com";

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByEmailAsync(email))
            .ReturnsAsync((User)null);

        var query = new GetUsersTakenCoursesQuery { Email = email };

        // Act
        Func<Task> act = async () => await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Entity \"User\" ({email}) not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoCoursesExist()
    {
        // Arrange
        var email = "testuser@example.com";
        var userId = Guid.NewGuid().ToString();

        var user = _fakers.Users
            .RuleFor(u => u.Id, userId)
            .RuleFor(u => u.Email, email)
            .Generate();

        var courses = new List<UserTakenCourses>();

        var mappedCourses = new List<UserTakenCourseDto>();

        mock.UnitOfWorkMock
            .Setup(uow => uow.Users.FindUserByEmailAsync(email))
            .ReturnsAsync(user);

        mock.UnitOfWorkMock
            .Setup(uow => uow.UserTakenCourses.GetUserTakenCoursesAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<UserTakenCourses>, IList<UserTakenCourseDto>>(courses))
            .ReturnsAsync(mappedCourses);

        var query = new GetUsersTakenCoursesQuery { Email = email };

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.UserTakenCourses.Should().BeEmpty();
    }
}