using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.ValueObjects;

public class AddressTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var street = "Main St";
        var number = "123";
        var neighborhood = "Downtown";
        var city = "Metropolis";
        var state = "SP";
        var zipCode = "12345-678";

        // Act
        var address = new Address(street, number, neighborhood, city, state, zipCode);

        // Assert
        address.Street.Should().Be(street);
        address.Number.Should().Be(number);
        address.Neighborhood.Should().Be(neighborhood);
        address.City.Should().Be(city);
        address.State.Should().Be(state);
        address.ZipCode.Should().Be(zipCode);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedAddress()
    {
        // Arrange
        var address = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");

        // Act
        var result = address.ToString();

        // Assert
        result.Should().Be("Main St, 123 - Downtown, Metropolis-SP, 12345-678");
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenAllPropertiesAreEqual()
    {
        // Arrange
        var a1 = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");
        var a2 = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");

        // Act & Assert
        a1.Equals(a2).Should().BeTrue();
        a2.Equals(a1).Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenAnyPropertyIsDifferent()
    {
        var a1 = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");
        var a2 = new Address("Other St", "123", "Downtown", "Metropolis", "SP", "12345-678");

        a1.Equals(a2).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForEqualAddresses()
    {
        var a1 = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");
        var a2 = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");

        a1.GetHashCode().Should().Be(a2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldBeDifferent_ForDifferentAddresses()
    {
        var a1 = new Address("Main St", "123", "Downtown", "Metropolis", "SP", "12345-678");
        var a2 = new Address("Other St", "123", "Downtown", "Metropolis", "SP", "12345-678");

        a1.GetHashCode().Should().NotBe(a2.GetHashCode());
    }
}
