using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.LoginUser;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CourseAppUserService_Tests.Mocks.User;

public class LoginUserMock: BaseMock
{
    public Mock<ITokenService> TokenServiceMock { get; private set; }
    private Mock<IUserStore<CourseAppUserService_Domain.Entities.User>> UserStoreMock { get; set; }
    public Mock<UserManager<CourseAppUserService_Domain.Entities.User>> UserManagerMock { get; private set; }
    public LoginUserCommandHandler Handler { get; private set; }

    public LoginUserMock()
    {
        TokenServiceMock = new Mock<ITokenService>();

        UserStoreMock = new Mock<IUserStore<CourseAppUserService_Domain.Entities.User>>();
        UserManagerMock = new Mock<UserManager<CourseAppUserService_Domain.Entities.User>>(UserStoreMock.Object);

        Handler = new LoginUserCommandHandler(UnitOfWorkMock.Object, TokenServiceMock.Object);
    }
}