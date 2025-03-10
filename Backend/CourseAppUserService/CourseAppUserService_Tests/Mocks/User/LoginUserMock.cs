using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.LoginUser;
using Microsoft.AspNetCore.Identity;
using Usr = CourseAppUserService_Domain.Entities.User;
using Moq;

namespace CourseAppUserService_Tests.Mocks.User;

public class LoginUserMock: BaseMock
{
    public Mock<ITokenService> TokenServiceMock { get; private set; }
    private Mock<IUserStore<Usr>> UserStoreMock { get; set; }
    public Mock<UserManager<Usr>> UserManagerMock { get; private set; }
    public LoginUserCommandHandler Handler { get; private set; }

    public LoginUserMock()
    {
        TokenServiceMock = new Mock<ITokenService>();

        UserStoreMock = new Mock<IUserStore<Usr>>();
        UserManagerMock = new Mock<UserManager<Usr>>(UserStoreMock.Object);

        Handler = new LoginUserCommandHandler(UnitOfWorkMock.Object, TokenServiceMock.Object);
    }
}