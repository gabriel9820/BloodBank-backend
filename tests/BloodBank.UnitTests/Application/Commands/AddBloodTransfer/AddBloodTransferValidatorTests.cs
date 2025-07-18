using BloodBank.Application.Commands.AddBloodTransfer;
using BloodBank.Core.Enums;
using BloodBank.UnitTests.Fakers;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.AddBloodTransfer;

public class AddBloodTransferValidatorTests
{
    private readonly AddBloodTransferValidator _validator;

    public AddBloodTransferValidatorTests()
    {
        _validator = new AddBloodTransferValidator();
    }

    [Fact]
    public void AddBloodTransferValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void AddBloodTransferValidator_ShouldFail_WhenTransferDateIsFuture()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        command.TransferDate = DateTime.UtcNow.AddDays(1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.TransferDate);
    }

    [Fact]
    public void AddBloodTransferValidator_ShouldFail_WhenTransferDateIsInvalid()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        command.TransferDate = default;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.TransferDate);
    }

    [Fact]
    public void AddBloodTransferValidator_ShouldFail_WhenBloodTypeIsInvalid()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        command.BloodType = (BloodType)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BloodType);
    }

    [Fact]
    public void AddBloodTransferValidator_ShouldFail_WhenRhFactorIsInvalid()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        command.RhFactor = (RhFactor)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.RhFactor);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void AddBloodTransferValidator_ShouldFail_WhenQuantityMLIsInvalid(int quantityML)
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        command.QuantityML = quantityML;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.QuantityML);
    }

    [Fact]
    public void AddBloodTransferValidator_ShouldFail_WhenHospitalIdIsInvalid()
    {
        // Arrange
        var command = new AddBloodTransferCommandFaker().Generate();
        command.HospitalId = default;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.HospitalId);
    }
}
