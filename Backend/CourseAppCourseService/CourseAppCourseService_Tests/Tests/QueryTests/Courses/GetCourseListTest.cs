using CourseAppCourseService_Application.Courses.Queries.GetCourseList;
using CourseAppCourseService_Domain;
using CourseAppCourseService_Tests.Fakers;
using CourseAppCourseService_Tests.Mocks.Courses;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseAppCourseService_Tests.Tests.QueryTests.Courses;

public class GetCourseListTest(GetCourseListMock mock) : IClassFixture<GetCourseListMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnCourseVm_WhenCoursesExist()
    {
        // Arrange
        var courses = _fakers.CourseFaker.Generate(3);

        var courseVm = new CourseVm
        {
            Courses = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            }).ToList()
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Courses.GetAllEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<Course>, CourseVm>(courses))
            .ReturnsAsync(courseVm);

        var query = new GetCourseListQuery();

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Courses.Should().HaveCount(3);
        result.Courses.Should().BeEquivalentTo(courseVm.Courses);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyCourseVm_WhenNoCoursesExist()
    {
        // Arrange
        var courses = new List<Course>();

        var courseVm = new CourseVm
        {
            Courses = new List<CourseDto>()
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Courses.GetAllEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<Course>, CourseVm>(courses))
            .ReturnsAsync(courseVm);

        var query = new GetCourseListQuery();

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Courses.Should().BeEmpty();
    }
}