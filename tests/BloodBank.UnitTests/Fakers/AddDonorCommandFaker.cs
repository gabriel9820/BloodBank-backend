using BloodBank.Application.Commands.AddDonor;
using BloodBank.Core.Constants;
using BloodBank.Core.Enums;

namespace BloodBank.UnitTests.Fakers;

public class AddDonorCommandFaker : Faker<AddDonorCommand>
{
    public AddDonorCommandFaker() : base("pt_BR")
    {
        RuleFor(c => c.FullName, f => f.Person.FullName);
        RuleFor(c => c.CellPhoneNumber, f => f.Phone.PhoneNumber("(##) 9####-####"));
        RuleFor(c => c.Email, f => f.Internet.Email());
        RuleFor(c => c.BirthDate, DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DonationRules.MIN_DONOR_AGE)));
        RuleFor(c => c.Gender, f => f.PickRandom<Gender>());
        RuleFor(c => c.Weight, f => f.Random.Decimal(DonationRules.MIN_DONOR_WEIGHT_KG, 100));
        RuleFor(c => c.BloodType, f => f.PickRandom<BloodType>());
        RuleFor(c => c.RhFactor, f => f.PickRandom<RhFactor>());
        RuleFor(c => c.Address, new AddressInputModelFaker().Generate());
    }
}
