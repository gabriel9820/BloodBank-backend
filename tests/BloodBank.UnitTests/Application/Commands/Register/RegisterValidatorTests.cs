using BloodBank.Application.Commands.Register;
using BloodBank.Core.Constants;
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
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = "(54) 91234-5678",
            Email = "user@email.com",
            Password = "Teste@123",
            Role = UserRoles.Admin
        };

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
        var command = new RegisterCommand()
        {
            FullName = fullName,
            CellPhoneNumber = "(54) 91234-5678",
            Email = "user@email.com",
            Password = "Teste@123",
            Role = UserRoles.Admin
        };

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
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = cellPhoneNumber,
            Email = "user@email.com",
            Password = "Teste@123",
            Role = UserRoles.Admin
        };

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
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = "(54) 91234-5678",
            Email = email,
            Password = "Teste@123",
            Role = UserRoles.Admin
        };

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
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = "(54) 91234-5678",
            Email = "user@email.com",
            Password = password,
            Role = UserRoles.Admin
        };

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
        var command = new RegisterCommand()
        {
            FullName = "Test User",
            CellPhoneNumber = "(54) 91234-5678",
            Email = "user@email.com",
            Password = "Teste@123",
            Role = role
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Role);
    }
}
