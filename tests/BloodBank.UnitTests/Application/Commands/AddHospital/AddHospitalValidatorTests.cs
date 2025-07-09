using BloodBank.Application.Commands.AddHospital;
using BloodBank.Application.DTOs.InputModels;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.AddHospital;

public class AddHospitalValidatorTests
{
    private readonly AddHospitalValidator _validator;

    public AddHospitalValidatorTests()
    {
        _validator = new AddHospitalValidator();
    }

    [Fact]
    public void AddHospitalValidator_ShouldPass_WhenCommandIsValid()
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
    public void AddHospitalValidator_ShouldFail_WhenNameIsInvalid(string name)
    {
        // Arrange
        var command = CreateValidCommand();
        command.Name = name;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("(11) 91234-5678")]
    [InlineData("(11)1234-5678")]
    public void AddHospitalValidator_ShouldFail_WhenLandlineNumberIsInvalid(string landlineNumber)
    {
        // Arrange
        var command = CreateValidCommand();
        command.LandlineNumber = landlineNumber;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.LandlineNumber);
    }

    [Fact]
    public void AddHospitalValidator_ShouldFail_WhenAddressIsInvalid()
    {
        // Arrange
        var command = CreateValidCommand();
        command.Address = null!;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Address);
    }

    private static AddHospitalCommand CreateValidCommand()
    {
        var address = new AddressInputModel
        {
            Street = "Test Street",
            Number = "123",
            Neighborhood = "Test Neighborhood",
            City = "Test City",
            State = "TS",
            ZipCode = "12345-768"
        };

        return new AddHospitalCommand
        {
            Name = "Test Hospital",
            LandlineNumber = "(11) 1234-5678",
            Address = address
        };
    }
}
