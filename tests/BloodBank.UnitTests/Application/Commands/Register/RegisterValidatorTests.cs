using BloodBank.Application.Commands.Register;
using BloodBank.UnitTests.Fakers;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.Register;

public class RegisterValidatorTests
{
    private readonly RegisterValidator _validator;

    public RegisterValidatorTests()
    {
        _validator = new RegisterValidator();
    }

    [Fact]
    public void RegisterValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new RegisterCommandFaker().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void RegisterValidator_ShouldFail_WhenFullNameIsInvalid(string fullName)
    {
        // Arrange
        var command = new RegisterCommandFaker()
            .RuleFor(c => c.FullName, fullName)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("(11)91234-5678")]
    [InlineData("(11) 1234-5678")]
    [InlineData("(11) 912345678")]
    public void RegisterValidator_ShouldFail_WhenCellPhoneNumberIsInvalid(string cellPhoneNumber)
    {
        // Arrange
        var command = new RegisterCommandFaker()
            .RuleFor(c => c.CellPhoneNumber, cellPhoneNumber)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.CellPhoneNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("invalid-email")]
    [InlineData("user@")]
    [InlineData("@domain")]
    [InlineData("@domain.com")]
    public void RegisterValidator_ShouldFail_WhenEmailIsInvalid(string email)
    {
        // Arrange
        var command = new RegisterCommandFaker()
            .RuleFor(c => c.Email, email)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("shortpw")]
    public void RegisterValidator_ShouldFail_WhenPasswordIsInvalid(string password)
    {
        // Arrange
        var command = new RegisterCommandFaker()
            .RuleFor(c => c.Password, password)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Password);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("InvalidRole")]
    public void RegisterValidator_ShouldFail_WhenRoleIsInvalid(string role)
    {
        // Arrange
        var command = new RegisterCommandFaker()
            .RuleFor(c => c.Role, role)
            .Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Role);
    }
}
