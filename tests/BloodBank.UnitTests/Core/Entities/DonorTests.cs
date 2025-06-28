using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.ValueObjects;

namespace BloodBank.UnitTests.Core.Entities;

public class DonorTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
    {
        // Arrange & Act
        var donor = CreateValidDonor();

        // Assert
        donor.Should().NotBeNull();
        donor.FullName.Should().Be("Test Donor");
        donor.CellPhoneNumber.Should().Be(new CellPhoneNumber("(54) 91234-5678"));
        donor.Email.Should().Be(new Email("donor@email.com"));
        donor.BirthDate.Should().Be(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE)));
        donor.Gender.Should().Be(Gender.Male);
        donor.Weight.Should().Be(DonationRules.MIN_DONOR_WEIGHT_KG);
        donor.BloodType.Should().Be(BloodType.O);
        donor.RhFactor.Should().Be(RhFactor.Positive);
        donor.Address.Should().Be(new Address("Test Street", "123", "Test Neighborhood", "Test City", "Test State", "12345-678"));
    }

    [Fact]
    public void Update_ShouldModifyProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var donor = CreateValidDonor();
        var updatedFullName = "Updated Donor";
        var updatedCellPhoneNumber = new CellPhoneNumber("(54) 98765-4321");
        var updatedEmail = new Email("updated@email.com");
        var updatedBirthDate = new DateOnly(1995, 5, 15);
        var updatedGender = Gender.Female;
        var updatedWeight = 65;
        var updatedBloodType = BloodType.A;
        var updatedRhFactor = RhFactor.Negative;
        var updatedAddress = new Address("Updated Street", "456", "Updated Neighborhood", "Updated City", "Updated State", "98765-432");

        // Act
        donor.Update(
            updatedFullName,
            updatedCellPhoneNumber,
            updatedEmail,
            updatedBirthDate,
            updatedGender,
            updatedWeight,
            updatedBloodType,
            updatedRhFactor,
            updatedAddress
        );

        // Assert
        donor.FullName.Should().Be(updatedFullName);
        donor.CellPhoneNumber.Should().Be(updatedCellPhoneNumber);
        donor.Email.Should().Be(updatedEmail);
        donor.BirthDate.Should().Be(updatedBirthDate);
        donor.Gender.Should().Be(updatedGender);
        donor.Weight.Should().Be(updatedWeight);
        donor.BloodType.Should().Be(updatedBloodType);
        donor.RhFactor.Should().Be(updatedRhFactor);
        donor.Address.Should().Be(updatedAddress);
    }

    [Fact]
    public void CanDonate_ShouldReturnTrue_WhenDonorIsEligible()
    {
        // Arrange
        var donor = CreateValidDonor();
        var lastDonationDate = DateTime.UtcNow.AddDays(-DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_MALE);

        // Act
        var canDonate = donor.CanDonate(lastDonationDate);

        // Assert
        canDonate.Should().BeTrue();
    }

    [Fact]
    public void CanDonate_ShouldReturnFalse_WhenDonorIsUnderage()
    {
        // Arrange
        var birthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE + 1));
        var donor = new Donor(
            "Underage Donor",
            new CellPhoneNumber("(54) 91234-5678"),
            new Email("underage.donor@email.com"),
            birthDate,
            Gender.Male,
            DonationRules.MIN_DONOR_WEIGHT_KG,
            BloodType.O,
            RhFactor.Positive,
            new Address("Underage Street", "123", "Underage Neighborhood", "Underage City", "Underage State", "12345-678")
        );

        // Act
        var canDonate = donor.CanDonate(null);

        // Assert
        canDonate.Should().BeFalse();
    }

    [Fact]
    public void CanDonate_ShouldReturnFalse_WhenDonorIsNotEligibleDueToWeight()
    {
        // Arrange
        var donor = new Donor(
            "Lightweight Donor",
            new CellPhoneNumber("(54) 91234-5678"),
            new Email("lightweight.donor@email.com"),
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE)),
            Gender.Male,
            DonationRules.MIN_DONOR_WEIGHT_KG - 1,
            BloodType.O,
            RhFactor.Positive,
            new Address("Lightweight Street", "123", "Lightweight Neighborhood", "Lightweight City", "Lightweight State", "12345-678")
        );

        // Act
        var canDonate = donor.CanDonate(null);

        // Assert
        canDonate.Should().BeFalse();
    }

    public static IEnumerable<object[]> LastDonationInvalidParameters => [
        [DateTime.UtcNow.AddDays(-DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_MALE + 1), Gender.Male],
        [DateTime.UtcNow.AddDays(-DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_FEMALE + 1), Gender.Female]
    ];

    [Theory]
    [MemberData(nameof(LastDonationInvalidParameters))]
    public void CanDonate_ShouldReturnFalse_WhenDonorIsNotEligibleDueToLastDonationDate(DateTime lastDonationDate, Gender gender)
    {
        // Arrange
        var donor = new Donor(
            "Test Donor",
            new CellPhoneNumber("(54) 91234-5678"),
            new Email("donor@email.com"),
            DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE)),
            gender,
            DonationRules.MIN_DONOR_WEIGHT_KG,
            BloodType.O,
            RhFactor.Positive,
            new Address("Test Street", "123", "Test Neighborhood", "Test City", "Test State", "12345-678")
        );

        // Act
        var canDonate = donor.CanDonate(lastDonationDate);

        // Assert
        canDonate.Should().BeFalse();
    }

    private static Donor CreateValidDonor() => new(
        "Test Donor",
        new CellPhoneNumber("(54) 91234-5678"),
        new Email("donor@email.com"),
        DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE)),
        Gender.Male,
        DonationRules.MIN_DONOR_WEIGHT_KG,
        BloodType.O,
        RhFactor.Positive,
        new Address("Test Street", "123", "Test Neighborhood", "Test City", "Test State", "12345-678")
    );
}
