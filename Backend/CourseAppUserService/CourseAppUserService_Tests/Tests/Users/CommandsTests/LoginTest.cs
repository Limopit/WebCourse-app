using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Users.Commands.LoginUser;
using CourseAppUserService_Domain;
using CourseAppUserService_Tests.Mocks;
using Moq;
using FluentAssertions;

namespace CourseAppUserService_Tests.Tests.Users.CommandsTests;

public class LoginTests : IClassFixture<LoginUserMock>
{
    private readonly LoginUserMock _loginUserMock;

    public LoginTests(LoginUserMock loginUserMock)
    {
        _loginUserMock = loginUserMock;
    }

    [Fact]
    public async Task Handle_ShouldReturnTokens_WhenUserIsValid()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "testuser",
            Email = "testuser@example.com"
        };
        
        _loginUserMock.UserManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _loginUserMock.UserManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(),
            It.IsAny<string>())).ReturnsAsync(true);
        _loginUserMock.TokenServiceMock.Setup(ts => ts.GenerateTokens(It.IsAny<User>(),
                _loginUserMock.UserManagerMock.Object, It.IsAny<CancellationToken>()))
            .ReturnsAsync(("access_token", "refresh_token"));

        // Act
        var result = await _loginUserMock.Handler.Handle(new LoginUserCommand 
            { Email = "testuser@example.com", Password = "password" }, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Item1.Should().Be("access_token");
        result.Item2.Should().Be("refresh_token");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new LoginUserCommand
        {
            Email = "nonexistentuser@example.com",
            Password = "password123"
        };
        var cancellationToken = new CancellationToken();

        _loginUserMock.UnitOfWorkMock.Setup(uow => uow.Users.FindUserByEmail(command.Email)).ReturnsAsync((User)null);
        _loginUserMock.UserManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync((User)null);

        // Act
        Func<Task> act = async () => await _loginUserMock.Handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("*not found*");
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPasswordIsInvalid()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "testuser",
            Email = "testuser@example.com"
        };
        var command = new LoginUserCommand
        {
            Email = "testuser@example.com",
            Password = "wrongpassword"
        };
        var cancellationToken = new CancellationToken();

        _loginUserMock.UnitOfWorkMock.Setup(uow => uow.Users.FindUserByEmail(command.Email)).ReturnsAsync(user);
        _loginUserMock.UserManagerMock.Setup(um => um.CheckPasswordAsync(user, command.Password)).ReturnsAsync(false);
        _loginUserMock.UserManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _loginUserMock.Handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Password is invalid");
    }
}
