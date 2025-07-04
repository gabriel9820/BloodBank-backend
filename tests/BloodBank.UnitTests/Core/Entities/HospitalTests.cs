using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.Entities;

public class HospitalTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var name = "Test Hospital";
        var landlineNumber = new LandlineNumber("(54) 1234-5678");
        var address = new Address("Test Street", "123", "Test Neighborhood", "Test City", "Test State", "98765-432");

        // Act
        var hospital = new Hospital(name, landlineNumber, address);

        // Assert
        hospital.Name.Should().Be(name);
        hospital.LandlineNumber.Should().Be(landlineNumber);
        hospital.Address.Should().Be(address);
    }

    [Fact]
    public void Update_ShouldModifyProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var hospital = new Hospital(
            "Test Hospital",
            new LandlineNumber("(54) 1234-5678"),
            new Address("Test Street", "123", "Test Neighborhood", "Test City", "Test State", "98765-432"));

        var newName = "Updated Hospital";
        var newLandlineNumber = new LandlineNumber("(54) 8765-4321");
        var newAddress = new Address("Updated Street", "456", "Updated Neighborhood", "Updated City", "Updated State", "12345-678");

        // Act
        hospital.Update(newName, newLandlineNumber, newAddress);

        // Assert
        hospital.Name.Should().Be(newName);
        hospital.LandlineNumber.Should().Be(newLandlineNumber);
        hospital.Address.Should().Be(newAddress);
    }
}
