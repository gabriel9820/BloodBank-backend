using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.Exceptions;
using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.Entities;

public class DonationTests
{
    public static IEnumerable<object[]> ValidConstructorParameters =>
    [
        [DateTime.UtcNow, DonationRules.MIN_DONATION_QUANTITY_ML],
        [DateTime.UtcNow.AddDays(-1), DonationRules.MAX_DONATION_QUANTITY_ML]
    ];

    [Theory]
    [MemberData(nameof(ValidConstructorParameters))]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided(DateTime donationDate, int quantity)
    {
        // Arrange
        var donor = CreateValidDonor();

        // Act
        var donation = new Donation(donationDate, quantity, donor);

        // Assert
        donation.DonationDate.Should().Be(donationDate);
        donation.QuantityML.Should().Be(quantity);
        donation.Donor.Should().Be(donor);
    }

    [Fact]
    public void Constructor_ShouldThrowFutureDateNotAllowedException_WhenDonationDateIsInTheFuture()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(1);
        var donor = CreateValidDonor();

        // Act
        Action act = () => new Donation(futureDate, DonationRules.MIN_DONATION_QUANTITY_ML, donor);

        // Assert
        act.Should().Throw<FutureDateNotAllowedException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    [InlineData(DonationRules.MIN_DONATION_QUANTITY_ML - 1)]
    [InlineData(DonationRules.MAX_DONATION_QUANTITY_ML + 1)]
    public void Constructor_ShouldThrowDonationQuantityOutOfRangeException_WhenQuantityIsOutOfRange(int quantity)
    {
        // Arrange
        var donor = CreateValidDonor();

        // Act
        Action act = () => new Donation(DateTime.UtcNow, quantity, donor);

        // Assert
        act.Should().Throw<DonationQuantityOutOfRangeException>();
    }

    private static Donor CreateValidDonor() => new(
        fullName: "Test Donor",
        cellPhoneNumber: new CellPhoneNumber("(54) 91234-5678"),
        email: new Email("donor@email.com"),
        birthDate: new DateOnly(1990, 1, 1),
        gender: Gender.Male,
        weight: 50,
        bloodType: BloodType.O,
        rhFactor: RhFactor.Positive,
        address: new Address("Test Street", "123", "Test Neighborhood", "Test City", "Test State", "12345-678")
    );
}
