using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;

namespace BloodBank.UnitTests.Core.Entities;

public class StockTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void AddToStock_ShouldIncreaseQuantity_WhenValidQuantity(int quantityToAdd)
    {
        // Arrange
        var stock = new Stock(
            BloodType.O,
            RhFactor.Negative,
            10
        );
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
        var stock = new Stock(
            BloodType.O,
            RhFactor.Negative,
            10
        );

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
        var stock = new Stock(
            BloodType.O,
            RhFactor.Negative,
            10
        );
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
        var stock = new Stock(
            BloodType.O,
            RhFactor.Negative,
            10
        );

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
        var stock = new Stock(
            BloodType.O,
            RhFactor.Negative,
            10
        );

        // Act
        Action act = () => stock.RemoveFromStock(quantityToRemove);

        // Assert
        act.Should().Throw<InsufficientStockException>();
    }
}
