using BloodBank.Application.Validators;
using BloodBank.UnitTests.Fakers;
using FluentValidation.TestHelper;

namespace BloodBank.UnitTests.Application.Validators;

public class AddressInputModelValidatorTests
{
    private readonly AddressInputModelValidator _validator;

    public AddressInputModelValidatorTests()
    {
        _validator = new AddressInputModelValidator();
    }

    [Fact]
    public void AddressInputModelValidator_ShouldPass_WhenModelIsValid()
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AddressInputModelValidator_ShouldReturnError_WhenStreetIsInvalid(string street)
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();
        model.Street = street;

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AddressInputModelValidator_ShouldReturnError_WhenNumberIsInvalid(string number)
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();
        model.Number = number;

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AddressInputModelValidator_ShouldReturnError_WhenNeighborhoodIsInvalid(string neighborhood)
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();
        model.Neighborhood = neighborhood;

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Neighborhood);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AddressInputModelValidator_ShouldReturnError_WhenCityIsInvalid(string city)
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();
        model.City = city;

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AddressInputModelValidator_ShouldReturnError_WhenStateIsInvalid(string state)
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();
        model.State = state;

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.State);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("12345678")]
    public void AddressInputModelValidator_ShouldReturnError_WhenZipCodeIsInvalid(string zipcode)
    {
        // Arrange
        var model = new AddressInputModelFaker().Generate();
        model.ZipCode = zipcode;

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode);
    }
}
