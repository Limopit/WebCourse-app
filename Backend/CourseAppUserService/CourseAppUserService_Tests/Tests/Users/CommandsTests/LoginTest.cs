using CourseAppUserService_Application.Users.Commands.LoginUser;
using CourseAppUserService_Domain;
using CourseAppUserService_Tests.Mocks;
using Moq;
using FluentAssertions;

namespace CourseAppUserService_Tests.Tests.Users.CommandsTests;

public class LoginTests(LoginUserMock loginUserMock) : IClassFixture<LoginUserMock>
{
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

        loginUserMock.UnitOfWorkMock.Setup(uow => uow.Users.FindUserByEmail("testuser@example.com"))
            .ReturnsAsync(user);
        loginUserMock.UnitOfWorkMock.Setup(uow => uow.Users.CheckPassword(user, "password"))
            .ReturnsAsync(true);
        loginUserMock.TokenServiceMock.Setup(ts => ts.GenerateTokens(user, It.IsAny<CancellationToken>()))
            .ReturnsAsync(("access_token", "refresh_token"));

        // Act
        var result = await loginUserMock.Handler.Handle(new LoginUserCommand 
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

        loginUserMock.UnitOfWorkMock.Setup(uow => uow.Users.FindUserByEmail(command.Email)).ReturnsAsync((User)null);
        loginUserMock.UserManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync((User)null);

        // Act
        Func<Task> act = async () => await loginUserMock.Handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Wrong email or password");
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

        loginUserMock.UnitOfWorkMock.Setup(uow => uow.Users.FindUserByEmail(command.Email)).ReturnsAsync(user);
        loginUserMock.UserManagerMock.Setup(um => um.CheckPasswordAsync(user, command.Password)).ReturnsAsync(false);
        loginUserMock.UserManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await loginUserMock.Handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Wrong email or password");
    }
}
