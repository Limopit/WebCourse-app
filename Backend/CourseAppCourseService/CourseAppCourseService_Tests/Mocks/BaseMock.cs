using CourseAppCourseService_Application.Interfaces;
using CourseAppCourseService_Application.Interfaces.Services;
using Moq;

namespace CourseAppCourseService_Tests.Mocks;

public class BaseMock
{
    public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new();
    public Mock<IMapperService> MapperServiceMock { get; private set; } = new();
}