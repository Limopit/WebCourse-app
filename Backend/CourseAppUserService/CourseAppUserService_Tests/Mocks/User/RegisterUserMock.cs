using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CourseAppUserService_Tests.Mocks.User;

public class RegisterUserMock: BaseMock
{
    public Mock<ITokenService> TokenServiceMock { get; private set; }
    private Mock<IUserStore<CourseAppUserService_Domain.Entities.User>> UserStoreMock { get; set; }
    public Mock<UserManager<CourseAppUserService_Domain.Entities.User>> UserManagerMock { get; private set; }
    public RegisterUserCommandHandler Handler { get; private set; }

    public RegisterUserMock()
    {
        TokenServiceMock = new Mock<ITokenService>();

        UserStoreMock = new Mock<IUserStore<CourseAppUserService_Domain.Entities.User>>();
        UserManagerMock = new Mock<UserManager<CourseAppUserService_Domain.Entities.User>>(
            UserStoreMock.Object);

        Handler = new RegisterUserCommandHandler(UnitOfWorkMock.Object);
    }
}
