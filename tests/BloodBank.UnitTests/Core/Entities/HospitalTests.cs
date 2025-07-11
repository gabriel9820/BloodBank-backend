using BloodBank.Core.Entities;
using BloodBank.Core.ValueObjects;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Core.Entities;

public class HospitalTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var name = "Test Hospital";
        var landlineNumber = new LandlineNumber("(54) 1234-5678");
        var address = new AddressFaker().Generate();

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
        var hospital = new HospitalFaker().Generate();
        var updatedName = "Updated Hospital";
        var updatedLandlineNumber = new LandlineNumber("(54) 8765-4321");
        var updatedAddress = new AddressFaker().Generate();

        // Act
        hospital.Update(updatedName, updatedLandlineNumber, updatedAddress);

        // Assert
        hospital.Name.Should().Be(updatedName);
        hospital.LandlineNumber.Should().Be(updatedLandlineNumber);
        hospital.Address.Should().Be(updatedAddress);
    }
}
