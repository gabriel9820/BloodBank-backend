using BloodBank.Application.Commands.UpdateDonor;
using BloodBank.Core.Enums;
using BloodBank.UnitTests.Fakers;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.UpdateDonor;

public class UpdateDonorValidatorTests
{
    private readonly UpdateDonorValidator _validator;

    public UpdateDonorValidatorTests()
    {
        _validator = new UpdateDonorValidator();
    }

    [Fact]
    public void UpdateDonorValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateDonorValidator_ShouldFail_WhenFullNameIsInvalid(string fullName)
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.FullName = fullName;

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
    public void UpdateDonorValidator_ShouldFail_WhenCellPhoneNumberIsInvalid(string cellPhoneNumber)
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.CellPhoneNumber = cellPhoneNumber;

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
    public void UpdateDonorValidator_ShouldFail_WhenEmailIsInvalid(string email)
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.Email = email;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void UpdateDonorValidator_ShouldFail_WhenBirthDateIsInvalid()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.BirthDate = default;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BirthDate);
    }

    [Fact]
    public void UpdateDonorValidator_ShouldFail_WhenGenderIsInvalid()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.Gender = (Gender)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Gender);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void UpdateDonorValidator_ShouldFail_WhenWeightIsInvalid(decimal weight)
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.Weight = weight;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Weight);
    }

    [Fact]
    public void UpdateDonorValidator_ShouldFail_WhenBloodTypeIsInvalid()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.BloodType = (BloodType)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BloodType);
    }

    [Fact]
    public void UpdateDonorValidator_ShouldFail_WhenRhFactorIsInvalid()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.RhFactor = (RhFactor)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.RhFactor);
    }

    [Fact]
    public void UpdateDonorValidator_ShouldFail_WhenAddressIsInvalid()
    {
        // Arrange
        var command = new UpdateDonorCommandFaker().Generate();
        command.Address = null!;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Address);
    }
}
