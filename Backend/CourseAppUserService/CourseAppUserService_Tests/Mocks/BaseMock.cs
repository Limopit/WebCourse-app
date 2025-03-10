using CourseAppUserService_Application.Interfaces;
using Moq;

namespace CourseAppUserService_Tests.Mocks;

public abstract class BaseMock
{
    public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new();
}