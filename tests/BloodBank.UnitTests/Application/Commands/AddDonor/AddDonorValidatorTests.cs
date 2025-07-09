using BloodBank.Application.Commands.AddDonor;
using BloodBank.Application.DTOs.InputModels;
using BloodBank.Core.Enums;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.AddDonor;

public class AddDonorValidatorTests
{
    private readonly AddDonorValidator _validator;

    public AddDonorValidatorTests()
    {
        _validator = new AddDonorValidator();
    }

    [Fact]
    public void AddDonorValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = CreateValidCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void AddDonorValidator_ShouldFail_WhenFullNameIsInvalid(string fullName)
    {
        // Arrange
        var command = CreateValidCommand();
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
    public void AddDonorValidator_ShouldFail_WhenCellPhoneNumberIsInvalid(string cellPhoneNumber)
    {
        // Arrange
        var command = CreateValidCommand();
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
    public void AddDonorValidator_ShouldFail_WhenEmailIsInvalid(string email)
    {
        // Arrange
        var command = CreateValidCommand();
        command.Email = email;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void AddDonorValidator_ShouldFail_WhenBirthDateIsInvalid()
    {
        // Arrange
        var command = CreateValidCommand();
        command.BirthDate = default;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BirthDate);
    }

    [Fact]
    public void AddDonorValidator_ShouldFail_WhenGenderIsInvalid()
    {
        // Arrange
        var command = CreateValidCommand();
        command.Gender = (Gender)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Gender);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void AddDonorValidator_ShouldFail_WhenWeightIsInvalid(decimal weight)
    {
        // Arrange
        var command = CreateValidCommand();
        command.Weight = weight;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Weight);
    }

    [Fact]
    public void AddDonorValidator_ShouldFail_WhenBloodTypeIsInvalid()
    {
        // Arrange
        var command = CreateValidCommand();
        command.BloodType = (BloodType)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.BloodType);
    }

    [Fact]
    public void AddDonorValidator_ShouldFail_WhenRhFactorIsInvalid()
    {
        // Arrange
        var command = CreateValidCommand();
        command.RhFactor = (RhFactor)999;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.RhFactor);
    }

    [Fact]
    public void AddDonorValidator_ShouldFail_WhenAddressIsInvalid()
    {
        // Arrange
        var command = CreateValidCommand();
        command.Address = null!;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Address);
    }

    private static AddDonorCommand CreateValidCommand()
    {
        var address = new AddressInputModel
        {
            Street = "Test Street",
            Number = "123",
            Neighborhood = "Test Neighborhood",
            City = "Test City",
            State = "TS",
            ZipCode = "12345-678"
        };

        return new AddDonorCommand
        {
            FullName = "Test Donor",
            CellPhoneNumber = "(54) 91234-5678",
            Email = "donor@email.com",
            BirthDate = new DateOnly(1990, 1, 1),
            Gender = Gender.Male,
            Weight = 70,
            BloodType = BloodType.O,
            RhFactor = RhFactor.Positive,
            Address = address
        };
    }
}
