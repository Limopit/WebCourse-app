using CourseAppUserService_Application.Interfaces;
using Moq;

namespace CourseAppUserService_Tests;

public class UserServiceDbContextFactory
{
    public readonly Mock<IUnitOfWork> UnitOfWorkMock = new();

    public void SetupSaveChangesAsync(int result = 1, CancellationToken cancellationToken = default)
    {
        UnitOfWorkMock.Setup(uow => uow.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(result);
    }
}