using BloodBank.Application.Commands.Login;
using BloodBank.UnitTests.Fakers;
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
    public void LoginValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new LoginCommandFaker().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-email")]
    public void LoginValidator_ShouldFail_WhenEmailIsInvalid(string email)
    {
        // Arrange
        var command = new LoginCommandFaker()
            .RuleFor(c => c.Email, email)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void LoginValidator_ShouldFail_WhenPasswordIsInvalid(string password)
    {
        // Arrange
        var command = new LoginCommandFaker()
            .RuleFor(c => c.Password, password)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Password);
    }
}
