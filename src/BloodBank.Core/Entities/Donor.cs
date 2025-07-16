using BloodBank.Core.Constants;
using BloodBank.Core.Enums;
using BloodBank.Core.ValueObjects;

namespace BloodBank.Core.Entities;

public class Donor(
    string fullName,
    CellPhoneNumber cellPhoneNumber,
    Email email,
    DateOnly birthDate,
    Gender gender,
    decimal weight,
    BloodType bloodType,
    RhFactor rhFactor,
    Address address) : BaseEntity
{
    public string FullName { get; private set; } = fullName;
    public CellPhoneNumber CellPhoneNumber { get; private set; } = cellPhoneNumber;
    public Email Email { get; private set; } = email;
    public DateOnly BirthDate { get; private set; } = birthDate;
    public Gender Gender { get; private set; } = gender;
    public decimal Weight { get; private set; } = weight;
    public BloodType BloodType { get; private set; } = bloodType;
    public RhFactor RhFactor { get; private set; } = rhFactor;
    public Address Address { get; private set; } = address;

    /* Navigation Properties */
    public IEnumerable<Donation> Donations { get; private set; } = new List<Donation>();

    protected Donor() : this(default!, default!, default!, default, default, default, default, default, default!) { }

    public void Update(
        string fullName,
        CellPhoneNumber cellPhoneNumber,
        Email email,
        DateOnly birthDate,
        Gender gender,
        decimal weight,
        BloodType bloodType,
        RhFactor rhFactor,
        Address address)
    {
        FullName = fullName;
        CellPhoneNumber = cellPhoneNumber;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Address = address;
    }

    public bool CanDonate(DateTime? lastDonationDate)
    {
        if ((DateTime.UtcNow.Year - BirthDate.Year) < DonationRules.MIN_DONOR_AGE)
            return false;

        if (Weight < DonationRules.MIN_DONOR_WEIGHT_KG)
            return false;

        if (lastDonationDate.HasValue)
        {
            var minInterval = Gender == Gender.Female
                ? DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_FEMALE
                : DonationRules.MIN_INTERVAL_BETWEEN_DONATIONS_DAYS_MALE;

            if ((DateTime.UtcNow - lastDonationDate.Value.Date).TotalDays < minInterval)
                return false;
        }

        return true;
    }
}
