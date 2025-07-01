using BloodBank.Application.Commands.Login;
using BloodBank.Application.Results;
using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Core.ValueObjects;
using BloodBank.Infrastructure.Auth;

namespace BloodBank.UnitTests.Application.Commands.Login;

public class LoginHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly LoginHandler _loginHandler;

    public LoginHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _authServiceMock = new Mock<IAuthService>();
        _loginHandler = new LoginHandler(_authServiceMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTokens_WhenUserIsValid()
    {
        // Arrange
        var command = new LoginCommand { Email = "user@email.com", Password = "Teste@123" };
        var expectedUser = CreateValidUser(command.Email);
        var expectedAccessToken = "access-token";
        var expectedRefreshToken = "refresh-token";

        _userRepositoryMock.Setup(ur => ur.GetByEmailAsync(command.Email)).ReturnsAsync(expectedUser);
        _authServiceMock.Setup(a => a.VerifyPassword(command.Password, expectedUser.PasswordHash)).Returns(true);
        _authServiceMock.Setup(a => a.GenerateJwtToken(expectedUser.Id.ToString(), expectedUser.Email.Value, expectedUser.Role)).Returns(expectedAccessToken);
        _authServiceMock.Setup(a => a.GenerateRefreshToken()).Returns(expectedRefreshToken);

        // Act
        var result = await _loginHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.AccessToken.Should().Be(expectedAccessToken);
        result.Data.RefreshToken.Should().Be(expectedRefreshToken);

        _userRepositoryMock.Verify(ur => ur.GetByEmailAsync(command.Email), Times.Once);
        _authServiceMock.Verify(a => a.VerifyPassword(command.Password, expectedUser.PasswordHash), Times.Once);
        _authServiceMock.Verify(a => a.GenerateJwtToken(expectedUser.Id.ToString(), expectedUser.Email.Value, expectedUser.Role), Times.Once);
        _authServiceMock.Verify(a => a.GenerateRefreshToken(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserNotFound()
    {
        // Arrange
        var command = new LoginCommand { Email = "user@email.com", Password = "Teste@123" };

        _userRepositoryMock.Setup(ur => ur.GetByEmailAsync(command.Email)).ReturnsAsync(() => null);

        // Act
        var result = await _loginHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidCredentials);

        _userRepositoryMock.Verify(ur => ur.GetByEmailAsync(command.Email), Times.Once);
        _authServiceMock.Verify(a => a.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _authServiceMock.Verify(a => a.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _authServiceMock.Verify(a => a.GenerateRefreshToken(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserIsNotActive()
    {
        // Arrange
        var command = new LoginCommand { Email = "user@email.com", Password = "Teste@123" };
        var expectedUser = CreateValidUser(command.Email);
        expectedUser.Deactivate();

        _userRepositoryMock.Setup(ur => ur.GetByEmailAsync(command.Email)).ReturnsAsync(expectedUser);

        // Act
        var result = await _loginHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidCredentials);

        _userRepositoryMock.Verify(ur => ur.GetByEmailAsync(command.Email), Times.Once);
        _authServiceMock.Verify(a => a.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _authServiceMock.Verify(a => a.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _authServiceMock.Verify(a => a.GenerateRefreshToken(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPasswordIsIncorrect()
    {
        // Arrange
        var command = new LoginCommand { Email = "user@email.com", Password = "Teste@123" };
        var expectedUser = CreateValidUser(command.Email);

        _userRepositoryMock.Setup(ur => ur.GetByEmailAsync(command.Email)).ReturnsAsync(expectedUser);
        _authServiceMock.Setup(a => a.VerifyPassword(command.Password, expectedUser.PasswordHash)).Returns(false);

        // Act
        var result = await _loginHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidCredentials);

        _userRepositoryMock.Verify(ur => ur.GetByEmailAsync(command.Email), Times.Once);
        _authServiceMock.Verify(a => a.VerifyPassword(command.Password, expectedUser.PasswordHash), Times.Once);
        _authServiceMock.Verify(a => a.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _authServiceMock.Verify(a => a.GenerateRefreshToken(), Times.Never);
    }

    private static User CreateValidUser(string email) => new(
        "Test User",
        new CellPhoneNumber("(54) 91234-5678"),
        new Email(email),
        "hashedPassword",
        UserRoles.Admin
    );
}
