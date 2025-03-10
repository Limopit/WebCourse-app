using CourseAppUserService_Application.Common.Exceptions;
using CourseAppUserService_Application.Users.Commands.RegisterUser;
using CourseAppUserService_Tests.Mocks.User;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Usr = CourseAppUserService_Domain.Entities.User;
using Moq;

namespace CourseAppUserService_Tests.Tests.Users.CommandsTests.User;

public class RegisterUserTests(RegisterUserMock registerUserMock) : IClassFixture<RegisterUserMock>
{
    [Fact]
    public async Task Handle_ShouldReturnUserId_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid().ToString();
        var command = new RegisterUserCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "newuser@example.com",
            Password = "Password123",
            Role = "User"
        };

        registerUserMock.UnitOfWorkMock.Setup(uow => uow.Users.UserRoleExistsAsync(command.Role))
            .ReturnsAsync(true);

        registerUserMock.UnitOfWorkMock.Setup(uow => uow.Users
                .AddUserAsync(It.Is<Usr>(u => u.Email == command.Email), command.Password))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<Usr, string>((u, p) => u.Id = expectedUserId);

        registerUserMock.UnitOfWorkMock.Setup(uow => uow.Users
                .GiveRoleAsync(It.IsAny<Usr>(), command.Role))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await registerUserMock.Handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expectedUserId);
    }

    [Fact]
    public async Task Handle_ShouldThrowRoleAssignmentException_WhenRoleDoesNotExist()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "newuser@example.com",
            Password = "Password123",
            Role = "NonExistentRole"
        };

        registerUserMock.UnitOfWorkMock.Setup(uow => uow.Users.UserRoleExistsAsync(command.Role))
            .ReturnsAsync(false);

        // Act
        Func<Task> act = async () => await registerUserMock.Handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<RoleAssignmentException>()
            .WithMessage("Role \"NonExistentRole\"  does not exist");
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUserCreationFails()
    {
        // Arrange
        var command = new RegisterUserCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "newuser@example.com",
            Password = "Password123",
            Role = "User"
        };

        registerUserMock.UnitOfWorkMock.Setup(uow => uow.Users.UserRoleExistsAsync(command.Role))
            .ReturnsAsync(true);

        registerUserMock.UnitOfWorkMock.Setup(uow => uow.Users.AddUserAsync(It.IsAny<Usr>(), command.Password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error creating user" }));

        // Act
        Func<Task> act = async () => await registerUserMock.Handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Error creating user");
    }
}
