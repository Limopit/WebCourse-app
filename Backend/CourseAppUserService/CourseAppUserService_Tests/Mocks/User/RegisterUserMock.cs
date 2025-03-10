using CourseAppUserService_Application.Users.Commands.RegisterUser;

namespace CourseAppUserService_Tests.Mocks.User;

public class RegisterUserMock: BaseMock
{
    public RegisterUserCommandHandler Handler { get; private set; }

    public RegisterUserMock()
    {
        Handler = new RegisterUserCommandHandler(UnitOfWorkMock.Object);
    }
}
