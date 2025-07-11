using BloodBank.Application.Commands.AddHospital;
using BloodBank.UnitTests.Fakers;
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
        var command = new AddHospitalCommandFaker().Generate();

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
        var command = new AddHospitalCommandFaker().Generate();
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
        var command = new AddHospitalCommandFaker().Generate();
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
        var command = new AddHospitalCommandFaker().Generate();
        command.Address = null!;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Address);
    }
}
