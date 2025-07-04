using BloodBank.Application.Commands.Register;
using BloodBank.Application.Results;
using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.Repositories;
using BloodBank.Infrastructure.Auth;

namespace BloodBank.UnitTests.Application.Commands.Register;

public class RegisterHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly RegisterHandler _registerHandler;

    public RegisterHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _uowMock = new Mock<IUnitOfWork>();
        _authServiceMock = new Mock<IAuthService>();
        _registerHandler = new RegisterHandler(_userRepositoryMock.Object, _uowMock.Object, _authServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldRegisterUser_WhenDataIsValid()
    {
        // Arrange
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = "(54) 91234-5678",
            Email = "user@email.com",
            Password = "Teste@123",
            Role = UserRoles.Admin
        };
        var passwordHash = "hashedPassword";

        _authServiceMock.Setup(a => a.HashPassword(command.Password)).Returns(passwordHash);
        _userRepositoryMock.Setup(ur => ur.IsEmailInUseAsync(command.Email)).ReturnsAsync(false);
        _userRepositoryMock.Setup(ur => ur.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber)).ReturnsAsync(false);
        _userRepositoryMock.Setup(ur => ur.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _uowMock.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _registerHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _authServiceMock.Verify(a => a.HashPassword(command.Password), Times.Once);
        _userRepositoryMock.Verify(ur => ur.IsEmailInUseAsync(command.Email), Times.Once);
        _userRepositoryMock.Verify(ur => ur.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber), Times.Once);
        _userRepositoryMock.Verify(ur => ur.AddAsync(It.Is<User>(u =>
            u.FullName == command.FullName &&
            u.CellPhoneNumber.Value == command.CellPhoneNumber &&
            u.Email.Value == command.Email &&
            u.PasswordHash == passwordHash &&
            u.Role == command.Role
        )), Times.Once);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public async Task Handle_ShouldReturnError_WhenCellPhoneNumberOrEmailInUse(bool isEmailInUse, bool isCellPhoneNumberInUse)
    {
        // Arrange
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = "(54) 91234-5678",
            Email = "user@email.com",
            Password = "Teste@123",
            Role = UserRoles.Admin
        };

        _userRepositoryMock.Setup(ur => ur.IsEmailInUseAsync(command.Email)).ReturnsAsync(isEmailInUse);
        _userRepositoryMock.Setup(ur => ur.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber)).ReturnsAsync(isCellPhoneNumberInUse);

        // Act
        var result = await _registerHandler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidData);

        _authServiceMock.Verify(a => a.HashPassword(It.IsAny<string>()), Times.Never);
        _userRepositoryMock.Verify(ur => ur.IsEmailInUseAsync(command.Email), Times.Once);
        _userRepositoryMock.Verify(ur => ur.IsCellPhoneNumberInUseAsync(command.CellPhoneNumber), Times.Once);
        _userRepositoryMock.Verify(ur => ur.AddAsync(It.IsAny<User>()), Times.Never);
        _uowMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
    }
}
