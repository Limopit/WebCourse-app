using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.LoginUser;
using CourseAppUserService_Domain;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CourseAppUserService_Tests.Mocks;

public class LoginUserMock
{
    public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }
    public Mock<ITokenService> TokenServiceMock { get; private set; }
    public Mock<IUserStore<User>> UserStoreMock { get; private set; }
    public Mock<UserManager<User>> UserManagerMock { get; private set; }
    public LoginUserCommandHandler Handler { get; private set; }

    public LoginUserMock()
    {
        UnitOfWorkMock = new Mock<IUnitOfWork>();
        TokenServiceMock = new Mock<ITokenService>();

        UserStoreMock = new Mock<IUserStore<User>>();
        UserManagerMock = new Mock<UserManager<User>>(
            UserStoreMock.Object, null, null, null, null, null, null, null, null
        );

        Handler = new LoginUserCommandHandler(UnitOfWorkMock.Object, TokenServiceMock.Object, UserManagerMock.Object);
    }
}