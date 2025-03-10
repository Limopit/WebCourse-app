using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.UserTakenCourse.Commands.CreateUserTakenCourse;
using Moq;

namespace CourseAppUserService_Tests.Mocks.UserTakenCourse;

public class CreateUserTakenCourseMock : BaseMock
{
    public Mock<IMapperService> MapperServiceMock { get; private set; }
    public CreateUserTakenCourseCommandHandler Handler { get; private set; }

    public CreateUserTakenCourseMock()
    {
        MapperServiceMock = new Mock<IMapperService>();
        Handler = new CreateUserTakenCourseCommandHandler(UnitOfWorkMock.Object, MapperServiceMock.Object);
    }
}