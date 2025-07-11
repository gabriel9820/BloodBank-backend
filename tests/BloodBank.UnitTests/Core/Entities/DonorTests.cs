using BloodBank.Core.Constants;
using BloodBank.Core.Entities;
using BloodBank.Core.Enums;
using BloodBank.Core.ValueObjects;
using BloodBank.UnitTests.Fakers;

namespace BloodBank.UnitTests.Core.Entities;

public class DonorTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var fullName = "Test Donor";
        var cellPhoneNumber = new CellPhoneNumber("(54) 91234-5678");
        var email = new Email("donor@email.com");
        var birthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE));
        var gender = Gender.Male;
        var weight = DonationRules.MIN_DONOR_WEIGHT_KG;
        var bloodType = BloodType.O;
        var rhFactor = RhFactor.Positive;
        var address = new AddressFaker().Generate();

        // Act
        var donor = new Donor(
            fullName,
            cellPhoneNumber,
            email,
            birthDate,
            gender,
            weight,
            bloodType,
            rhFactor,
            address
        );

        // Assert
        donor.Should().NotBeNull();
        donor.FullName.Should().Be(fullName);
        donor.CellPhoneNumber.Should().Be(cellPhoneNumber);
        donor.Email.Should().Be(email);
        donor.BirthDate.Should().Be(birthDate);
        donor.Gender.Should().Be(gender);
        donor.Weight.Should().Be(weight);
        donor.BloodType.Should().Be(bloodType);
        donor.RhFactor.Should().Be(rhFactor);
        donor.Address.Should().Be(address);
    }

    [Fact]
    public void Update_ShouldModifyProperties_WhenValidParametersAreProvided()
    {
        // Arrange
        var donor = new DonorFaker().Generate();
        var updatedFullName = "Updated Donor";
        var updatedCellPhoneNumber = new CellPhoneNumber("(54) 98765-4321");
        var updatedEmail = new Email("updated@email.com");
        var updatedBirthDate = new DateOnly(1995, 5, 15);
        var updatedGender = Gender.Female;
        var updatedWeight = 65;
        var updatedBloodType = BloodType.A;
        var updatedRhFactor = RhFactor.Negative;
        var updatedAddress = new AddressFaker().Generate();

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

    [Theory]
    [InlineData(Gender.Male, DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_MALE)]
    [InlineData(Gender.Female, DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_FEMALE)]
    public void CanDonate_ShouldReturnTrue_WhenDonorIsEligible(Gender gender, int minIntervalDays)
    {
        // Arrange
        var donor = new DonorFaker()
            .RuleFor(d => d.Gender, gender)
            .Generate();

        var lastDonationDate = DateTime.UtcNow.AddDays(-minIntervalDays);

        // Act
        var canDonate = donor.CanDonate(lastDonationDate);

        // Assert
        canDonate.Should().BeTrue();
    }

    [Fact]
    public void CanDonate_ShouldReturnFalse_WhenDonorIsUnderage()
    {
        // Arrange
        var donor = new DonorFaker()
            .RuleFor(d => d.BirthDate, f => DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE + 1)))
            .Generate();

        // Act
        var canDonate = donor.CanDonate(null);

        // Assert
        canDonate.Should().BeFalse();
    }

    [Fact]
    public void CanDonate_ShouldReturnFalse_WhenDonorIsNotEligibleDueToWeight()
    {
        // Arrange
        var donor = new DonorFaker()
            .RuleFor(d => d.Weight, f => f.Random.Int(1, DonationRules.MIN_DONOR_WEIGHT_KG - 1))
            .Generate();

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
        var donor = new DonorFaker()
            .RuleFor(d => d.Gender, gender)
            .Generate();

        // Act
        var canDonate = donor.CanDonate(lastDonationDate);

        // Assert
        canDonate.Should().BeFalse();
    }
}
