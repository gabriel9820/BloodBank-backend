using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Core.Entities;

public class BloodTransferTests
{
    public static IEnumerable<object[]> ValidConstructorParameters =>
    [
        [DateTime.UtcNow, 470],
        [DateTime.UtcNow.AddDays(-1), 420]
    ];

    [Theory]
    [MemberData(nameof(ValidConstructorParameters))]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided(DateTime transferDate, int quantityML)
    {
        // Arrange
        var bloodType = BloodType.A;
        var rhFactor = RhFactor.Positive;
        var hospital = new HospitalFaker().Generate();

        // Act
        var bloodTransfer = new BloodTransfer(transferDate, bloodType, rhFactor, quantityML, hospital);

        // Assert
        bloodTransfer.TransferDate.Should().Be(transferDate);
        bloodTransfer.BloodType.Should().Be(bloodType);
        bloodTransfer.RhFactor.Should().Be(rhFactor);
        bloodTransfer.QuantityML.Should().Be(quantityML);
        bloodTransfer.Hospital.Should().Be(hospital);
    }

    [Fact]
    public void Constructor_ShouldThrowFutureDateNotAllowedException_WhenTransferDateIsInTheFuture()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(1);
        var hospital = new HospitalFaker().Generate();

        // Act
        Action act = () => new BloodTransfer(
            futureDate,
            BloodType.O,
            RhFactor.Negative,
            470,
            hospital
        );

        // Assert
        act.Should().Throw<FutureDateNotAllowedException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-400)]
    public void Constructor_ShouldThrowInvalidQuantityException_WhenQuantityIsInvalid(int quantityML)
    {
        // Arrange
        var hospital = new HospitalFaker().Generate();

        // Act
        Action act = () => new BloodTransfer(
            DateTime.UtcNow,
            BloodType.O,
            RhFactor.Negative,
            quantityML,
            hospital
        );

        // Assert
        act.Should().Throw<InvalidQuantityException>();
    }
}
