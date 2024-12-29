using CourseAppUserService_Application.Interfaces;
using CourseAppUserService_Application.Interfaces.Services;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using CourseAppUserService_Domain;
using CourseAppUserService_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CourseAppUserService_Tests.Mocks;

public class RegisterUserMock
{
    public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }
    public Mock<ITokenService> TokenServiceMock { get; private set; }
    private Mock<IUserStore<User>> UserStoreMock { get; set; }
    public Mock<UserManager<User>> UserManagerMock { get; private set; }
    public RegisterUserCommandHandler Handler { get; private set; }

    public RegisterUserMock()
    {
        UnitOfWorkMock = new Mock<IUnitOfWork>();
        TokenServiceMock = new Mock<ITokenService>();

        UserStoreMock = new Mock<IUserStore<User>>();
        UserManagerMock = new Mock<UserManager<User>>(
            UserStoreMock.Object);

        Handler = new RegisterUserCommandHandler(UnitOfWorkMock.Object);
    }
}
