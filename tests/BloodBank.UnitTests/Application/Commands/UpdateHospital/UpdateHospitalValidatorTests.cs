using BloodBank.Application.Commands.UpdateHospital;
using BloodBank.UnitTests.Fakers;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Commands.UpdateHospital;

public class UpdateHospitalValidatorTests
{
    private readonly UpdateHospitalValidator _validator;

    public UpdateHospitalValidatorTests()
    {
        _validator = new UpdateHospitalValidator();
    }

    [Fact]
    public void UpdateHospitalValidator_ShouldPass_WhenCommandIsValid()
    {
        // Arrange
        var command = new UpdateHospitalCommandFaker().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateHospitalValidator_ShouldFail_WhenNameIsInvalid(string name)
    {
        // Arrange
        var command = new UpdateHospitalCommandFaker().Generate();
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
    public void UpdateHospitalValidator_ShouldFail_WhenLandlineNumberIsInvalid(string landlineNumber)
    {
        // Arrange
        var command = new UpdateHospitalCommandFaker().Generate();
        command.LandlineNumber = landlineNumber;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.LandlineNumber);
    }

    [Fact]
    public void UpdateHospitalValidator_ShouldFail_WhenAddressIsInvalid()
    {
        // Arrange
        var command = new UpdateHospitalCommandFaker().Generate();
        command.Address = null!;

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Address);
    }
}
