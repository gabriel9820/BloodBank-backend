using BloodBank.Application.Commands.Login;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.Login;

public class LoginValidatorTests
{
    private readonly LoginValidator _validator;

    public LoginValidatorTests()
    {
        _validator = new LoginValidator();
    }

    [Fact]
    public void LoginCommand_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new LoginCommand { Email = "user@email.com", Password = "123456" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-email")]
    public void LoginCommand_ShouldFail_WhenEmailIsInvalid(string email)
    {
        // Arrange
        var command = new LoginCommand { Email = email, Password = "123456" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void LoginCommand_ShouldFail_WhenPasswordIsInvalid(string password)
    {
        // Arrange
        var command = new LoginCommand { Email = "user@email.com", Password = password };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Password);
    }
}
