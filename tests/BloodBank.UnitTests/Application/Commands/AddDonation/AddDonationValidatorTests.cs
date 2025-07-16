using BloodBank.Application.Commands.AddDonation;
using BloodBank.Core.Constants;
using BloodBank.UnitTests.Fakers;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.AddDonation;

public class AddDonationValidatorTests
{
    private readonly AddDonationValidator _validator;

    public AddDonationValidatorTests()
    {
        _validator = new AddDonationValidator();
    }

    [Fact]
    public void AddDonationValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void AddDonationValidator_ShouldFail_WhenDonationDateIsFuture()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        command.DonationDate = DateTime.UtcNow.AddDays(1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.DonationDate);
    }

    [Fact]
    public void AddDonationValidator_ShouldFail_WhenDonationDateIsInvalid()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        command.DonationDate = default;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.DonationDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(DonationRules.MIN_DONATION_QUANTITY_ML - 1)]
    [InlineData(DonationRules.MAX_DONATION_QUANTITY_ML + 1)]
    public void AddDonationValidator_ShouldFail_WhenQuantityMLIsInvalid(int quantityML)
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        command.QuantityML = quantityML;

        // Act  
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.QuantityML);
    }

    [Fact]
    public void AddDonationValidator_ShouldFail_WhenDonorIdIsInvalid()
    {
        // Arrange
        var command = new AddDonationCommandFaker().Generate();
        command.DonorId = default;

        // Act  
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.DonorId);
    }
}
