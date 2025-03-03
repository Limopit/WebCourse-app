using CourseAppUserService_Application.Interfaces;
using Moq;

namespace CourseAppUserService_Tests.Mocks;

public abstract class BaseMock
{
    public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new();

    public void SetupSaveChangesAsync(int result = 1, CancellationToken cancellationToken = default)
    {
        UnitOfWorkMock.Setup(uow => uow.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(result);
    }
}