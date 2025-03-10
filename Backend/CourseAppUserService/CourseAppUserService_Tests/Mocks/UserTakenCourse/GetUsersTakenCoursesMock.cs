using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserTakenCourse.Queries.GetUsersTakenCourses;
using Moq;

namespace CourseAppUserService_Tests.Mocks.UserTakenCourse;

public class GetUsersTakenCoursesMock: BaseMock
{
    public Mock<IMapperService> MapperServiceMock { get; private set; }
    public GetUsersTakenCoursesQueryHandler Handler { get; private set; }

    public GetUsersTakenCoursesMock()
    {
        MapperServiceMock = new Mock<IMapperService>();
        Handler = new GetUsersTakenCoursesQueryHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}