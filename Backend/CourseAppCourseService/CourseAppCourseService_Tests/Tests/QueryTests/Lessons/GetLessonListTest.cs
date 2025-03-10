using CourseAppCourseService_Application.Lessons.Queries.GetLessonList;
using CourseAppCourseService_Domain;
using CourseAppCourseService_Tests.Fakers;
using CourseAppCourseService_Tests.Mocks.Lessons;
using FluentAssertions;
using Moq;
using Xunit;

namespace CourseAppCourseService_Tests.Tests.QueryTests.Lessons;

public class GetLessonListTest(GetLessonListMock mock): IClassFixture<GetLessonListMock>
{
    private readonly FakerContext _fakers = new();

    [Fact]
    public async Task Handle_ShouldReturnCourseVm_WhenCoursesExist()
    {
        // Arrange
        var lessons = _fakers.LessonFaker.Generate(3);

        var lessonVm = new LessonVm()
        {
            Lessons = lessons.Select(c => new LessonDto()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            }).ToList()
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Lessons.GetAllEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(lessons);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<Lesson>, LessonVm>(lessons))
            .ReturnsAsync(lessonVm);

        var query = new GetLessonListQuery();

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Lessons.Should().HaveCount(3);
        result.Lessons.Should().BeEquivalentTo(lessonVm.Lessons);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyCourseVm_WhenNoCoursesExist()
    {
        // Arrange
        var courses = new List<Lesson>();

        var courseVm = new LessonVm()
        {
            Lessons = new List<LessonDto>()
        };

        mock.UnitOfWorkMock
            .Setup(uow => uow.Lessons.GetAllEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        mock.MapperServiceMock
            .Setup(mapper => mapper.MapAsync<List<Lesson>, LessonVm>(courses))
            .ReturnsAsync(courseVm);

        var query = new GetLessonListQuery();

        // Act
        var result = await mock.Handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Lessons.Should().BeEmpty();
    }
}