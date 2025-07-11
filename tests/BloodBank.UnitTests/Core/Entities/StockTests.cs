using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Core.Entities;

public class StockTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParameters()
    {
        // Arrange
        var bloodType = BloodType.O;
        var rhFactor = RhFactor.Negative;
        int quantityML = 0;

        // Act
        var stock = new Stock(bloodType, rhFactor, quantityML);

        // Assert
        stock.BloodType.Should().Be(bloodType);
        stock.RhFactor.Should().Be(rhFactor);
        stock.QuantityML.Should().Be(quantityML);
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidQuantityException_WhenNegativeQuantity()
    {
        // Act
        Action act = () => new Stock(BloodType.O, RhFactor.Negative, -5);

        // Assert
        act.Should().Throw<InvalidQuantityException>();
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void AddToStock_ShouldIncreaseQuantity_WhenValidQuantity(int quantityToAdd)
    {
        // Arrange
        var stock = new StockFaker()
            .RuleFor(s => s.QuantityML, 10)
            .Generate();
        int expectedQuantity = stock.QuantityML + quantityToAdd;

        // Act
        stock.AddToStock(quantityToAdd);

        // Assert
        stock.QuantityML.Should().Be(expectedQuantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void AddToStock_ShouldThrowInvalidStockQuantityException_WhenInvalidQuantity(int quantityToAdd)
    {
        // Arrange
        var stock = new StockFaker()
            .RuleFor(s => s.QuantityML, 10)
            .Generate();

        // Act
        Action act = () => stock.AddToStock(quantityToAdd);

        // Assert
        act.Should().Throw<InvalidQuantityException>();
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void RemoveFromStock_ShouldDecreaseQuantity_WhenValidQuantity(int quantityToRemove)
    {
        // Arrange
        var stock = new StockFaker()
            .RuleFor(s => s.QuantityML, 10)
            .Generate();
        int expectedQuantity = stock.QuantityML - quantityToRemove;

        // Act
        stock.RemoveFromStock(quantityToRemove);

        // Assert
        stock.QuantityML.Should().Be(expectedQuantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void RemoveFromStock_ShouldThrowInvalidStockQuantityException_WhenInvalidQuantity(int quantityToRemove)
    {
        // Arrange
        var stock = new StockFaker()
            .RuleFor(s => s.QuantityML, 10)
            .Generate();

        // Act
        Action act = () => stock.RemoveFromStock(quantityToRemove);

        // Assert
        act.Should().Throw<InvalidQuantityException>();
    }

    [Theory]
    [InlineData(15)]
    [InlineData(20)]
    public void RemoveFromStock_ShouldThrowInsufficientStockException_WhenQuantityExceedsAvailable(int quantityToRemove)
    {
        // Arrange
        var stock = new StockFaker()
            .RuleFor(s => s.QuantityML, 10)
            .Generate();

        // Act
        Action act = () => stock.RemoveFromStock(quantityToRemove);

        // Assert
        act.Should().Throw<InsufficientStockException>();
    }
}
